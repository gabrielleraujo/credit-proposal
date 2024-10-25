using System.Text.Json.Serialization;

namespace CreditProposal.Application.Messaging.CustomerMainAddressRegistered.PublishEvent
{
    public class CreditProposalHasCreditReleasedEvent
    {
        public CreditProposalHasCreditReleasedEvent(
            Guid customerId,
            Guid proposalCreditId,
            string firstName,
            string lastName,
            string email,
            decimal creditLimitReleased, // baseado no valor do limite o ms de cartao irá escolher o cartao de crédito que será liberado.
            DateTime timestamp)
        {
            CustomerId = customerId;
            ProposalCreditId = proposalCreditId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreditLimitReleased = creditLimitReleased;
            Timestamp = timestamp;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }

        [JsonPropertyName("customer_id")]
        public Guid ProposalCreditId { get; private set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; private set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; private set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; private set; } = string.Empty;

        [JsonPropertyName("credit-limit-released")]
        public decimal CreditLimitReleased { get; private set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; private set; }
    }
}