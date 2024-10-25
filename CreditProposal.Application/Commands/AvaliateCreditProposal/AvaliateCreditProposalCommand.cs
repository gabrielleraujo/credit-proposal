using CreditProposal.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent;
using CreditProposal.Application.Validations;
using FluentValidation.Results;
using MediatR;

namespace CreditProposal.Application.Commands.AvaliateCreditProposalCommand
{
    public class AvaliateCreditProposalCommand : Command, IRequest<ValidationResult>
    {
        public AvaliateCreditProposalCommand(
            CustomerMainAddressRegisteredEvent @event)
        {
            Event = @event;
        }

        public CustomerMainAddressRegisteredEvent Event { get; private set; }

        public override bool IsValid() 
        {
            ValidationResult = new AvaliateCreditProposalCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
