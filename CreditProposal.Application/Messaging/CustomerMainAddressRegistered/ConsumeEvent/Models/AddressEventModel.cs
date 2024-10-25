using Newtonsoft.Json;

namespace CreditProposal.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent.Models
{
    public class AddressEventModel
    {
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; } = string.Empty;

        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; } = string.Empty; 

        [JsonProperty("street")]
        public string Street { get; set; } = string.Empty;

        [JsonProperty("number")]
        public uint Number { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; } = string.Empty;
    }
}