namespace CreditProposal.Infrastructure.Messaging
{
    public interface IMessageBusSetupServer
    {
        void Setup();
        void Dispose();
    }
}
