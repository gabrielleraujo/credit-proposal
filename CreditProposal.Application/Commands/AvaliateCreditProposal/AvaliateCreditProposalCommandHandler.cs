using MediatR;
using System.Text.Json;
using CreditProposal.Domain.Repositories;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using CreditProposal.Domain.Messaging;
using CreditProposal.Domain.Models.ValueObject;
using CreditProposal.Domain.Models.Entities;
using CreditProposal.Application.Messaging.CustomerMainAddressRegistered.PublishEvent;

namespace CreditProposal.Application.Commands.AvaliateCreditProposalCommand
{
    public class AvaliateCreditProposalCommandHandler : 
        CommandHandler<AvaliateCreditProposalCommandHandler>,
        IRequestHandler<AvaliateCreditProposalCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly ICreditProposalRepository _repository;
        private readonly IMessageBusPublisher _messageBus;

        public AvaliateCreditProposalCommandHandler(
            ILogger<AvaliateCreditProposalCommandHandler> logger,
            IMediator mediator,
            ICreditProposalRepository repository,
            IMessageBusPublisher messageBus) : base(logger)
        {
            _mediator = mediator;
            _repository = repository;
            _messageBus = messageBus;
        }
        
        public async Task<ValidationResult> Handle(AvaliateCreditProposalCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid\nEnd steps.");
                return command.ValidationResult;
            }

            var address = new Address(
                command.Event.Address.PostalCode,
                command.Event.Address.State,
                command.Event.Address.City,
                command.Event.Address.Neighborhood,
                command.Event.Address.Street,
                command.Event.Address.Number,
                command.Event.Address.Complement
            );

            var customer = await _repository.FindCustomerByAsync(x => x.Id == command.Event.CustomerId);

            if (customer == null)
            {
                _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} Customer not registered yet.");

                // ---- Step Register Customer if it was not regitered.
                customer = new CustomerEntity(
                    id: command.Event.CustomerId,
                    name: new Name(command.Event.FirstName, command.Event.LastName),
                    mainEmail: new Email(command.Event.Email),
                    address: address
                );
                await _repository.AddCustomerAsync(customer);

                _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} step register customer successfully added not commited");
            }
            
            // ---- Step Avaliate Credit Offer to the customer
            var creditProposal = new CreditProposalEntity(
                Guid.NewGuid(),
                customer
            );

            // TODO: Verificar no banco se o cliente já possui uma oferta liberada. Se tiver => nao continuar o processo. Se nao tiver => AvaliateCreditOffer.
            var creditProposalDB = await _repository.FindByAsync(x => x.Customer.Id == command.Event.CustomerId);
            if (creditProposalDB != null)
            {
                var message = $"There is already a credit proposal for the customer with creditLimitReleased : {creditProposalDB.CreditLimitReleased}";
                _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} {message}\nEnd steps.");
                AddError(message);
                return ValidationResult;
            }

            creditProposal.AvaliateCreditOffer();

            if (creditProposal.IsCreditReleased == false)
            {
                _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} step avaliate credit offer completed (Offer was NOT RELEASED to the customer)\nEnd steps.");
                return ValidationResult;
            }

            _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} step avaliate credit offer completed (Offer was RELEASED to the customer) with creditLimitReleased {creditProposal.CreditLimitReleased}");

            _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} Add new Credit Proposal to the customer {command.Event.CustomerId}");
            
            await _repository.AddAsync(creditProposal);
            await _repository.CommitAsync();

            // Public evento de proposta de crédito teve limite liberado
            var creditProposalHasCreditReleased = new CreditProposalHasCreditReleasedEvent(
                customerId: command.Event.CustomerId,
                proposalCreditId: creditProposal.Id,
                firstName: command.Event.FirstName,
                lastName: command.Event.LastName,
                email: command.Event.Email,
                creditLimitReleased: creditProposal.CreditLimitReleased,
                timestamp: DateTime.UtcNow
            );

            string routingKey = "credit_proposal_has_credit_released_event_key";  // Definir a chave de roteamento adequada
            _messageBus.PublishMessage(creditProposalHasCreditReleased, routingKey);  // Publica a mensagem no RabbitMQ

            _logger.LogInformation($"{nameof(AvaliateCreditProposalCommandHandler)} All steps was successfully completed\nEnd steps.");

            return ValidationResult;
        }
    }
}
