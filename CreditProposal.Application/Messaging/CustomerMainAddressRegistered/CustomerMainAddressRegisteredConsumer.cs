using System;
using Newtonsoft.Json;
using Polly;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CreditProposal.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent;
using CreditProposal.Application.Commands.AvaliateCreditProposalCommand;
using FluentValidation.Results;

namespace CreditProposal.Application.Messaging.CustomerMainAddressRegistered
{
    public class CustomerMainAddressRegisteredConsumer : RabbitMqConsumerBackgroundServiceTemplate, IDisposable
    {
        private const string _routingKeySubscribe = "customer_main_address_registered_event_key";

        public CustomerMainAddressRegisteredConsumer(
            IConnection connection, IModel channel, ILogger<CustomerMainAddressRegisteredConsumer> logger, IServiceProvider serviceProvider)
            : base(connection, channel, logger, serviceProvider,
                  exchange: "customer-service-exchange", // Mesma exchange do outro microsserviço
                  queue: "avaliate_credit_proposal_queue", // 
                  queueDeadLetter: "avaliate_credit_proposal_queue_dead_letter_queue")
        { 
            // Vincular a fila ao exchange com a chave de roteamento "customer_main_address_registered_event_key"
            _channel.QueueBind(queue: _queue, 
                              exchange: _exchange, 
                              routingKey: _routingKeySubscribe);
        }

        protected override async Task<ValidationResult> ProcessMessage(string message)
        {   
            _logger.LogInformation($"{nameof(CustomerMainAddressRegisteredConsumer)} - START ===============================================");
         
            // Tentar processar a mensagem
            _logger.LogInformation("Processando proposta de crédito para: {message}", message);

            // Simular falha no processamento
            // throw new Exception("Falha simulada!");

            var @event = JsonConvert.DeserializeObject<CustomerMainAddressRegisteredEvent>(message); // ESTÁ DANDO ERRO PARA CONVERTER

            // // Lógica para verificar e processar a liberação de crédito
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new AvaliateCreditProposalCommand(@event);
            var response = await mediator.Send(command);
            _logger.LogInformation("Finalizado o processamento da proposta de crédito para o cliente: {CustomerId}, response isValid: {IsValid}", @event.CustomerId, response.IsValid);
            _logger.LogInformation($"{nameof(CustomerMainAddressRegisteredConsumer)} - END ===============================================");
            return response;
        }

        protected virtual void Dispose(bool disposing)
        {
            RunDispose(disposing);
        }
    }
}
