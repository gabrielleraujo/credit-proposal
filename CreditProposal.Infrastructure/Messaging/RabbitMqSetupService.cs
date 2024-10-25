using RabbitMQ.Client;

namespace CreditProposal.Infrastructure.Messaging
{
    public class RabbitMqSetupService : IMessageBusSetupServer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string _exchange = "customer-service-exchange";
        private const string _queueAvaliateCreditProposal = "avaliate_credit_proposal_queue";
        private const string _queueAvaliateCreditProposalDeadLetter = "avaliate_credit_proposal_queue_dead_letter_queue";

        public RabbitMqSetupService(IConnection connection, IModel channel)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public void Setup()
        {
            SetupExchange();
            SetupQueues();
        }

        private void SetupExchange()
        {
            // Declara a exchange principal
            _channel.ExchangeDeclare(_exchange, ExchangeType.Direct, durable: true);
        }

        private void SetupQueues()
        {
            // Configurações da dead-letter queue
            var args = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", _exchange }, // Opcionalmente, você pode configurar um exchange para a DLQ
                { "x-message-ttl", 60000 } // Tempo de vida da mensagem na DLQ (opcional)
            };

            // Declarar a dead-letter queue
            _channel.QueueDeclare(_queueAvaliateCreditProposalDeadLetter, durable: true, exclusive: false, autoDelete: false, arguments: args);

            // Fila principal, que receberá as mensagens
            _channel.QueueDeclare(queue: _queueAvaliateCreditProposal,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: args);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
