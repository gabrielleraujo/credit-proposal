using System.Text.Json.Serialization;
using CreditProposal.Application.ViewModels;
using MediatR;

namespace CreditProposal.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQuery: IRequest<CustomerViewModel?>
    {
        public GetCustomerByIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }
    }
}
