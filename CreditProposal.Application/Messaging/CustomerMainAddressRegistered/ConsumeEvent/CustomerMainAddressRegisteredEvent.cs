using Newtonsoft.Json;
using CreditProposal.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent.Models;

namespace CreditProposal.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent
{
    public class CustomerMainAddressRegisteredEvent
    {
        [JsonProperty("customer_id")]
        public Guid CustomerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("address")]
        public AddressEventModel Address { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
