using System.Text.Json.Serialization;
using CreditProposal.Application.ViewModels;
using MediatR;

namespace CreditProposal.Application.Queries.GetAllCustomers
{
    public class GetCreditProposalsByCustomerIdQuery: IRequest<IEnumerable<CreditProposalViewModel>>
    {
        public GetCreditProposalsByCustomerIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }
    }
}