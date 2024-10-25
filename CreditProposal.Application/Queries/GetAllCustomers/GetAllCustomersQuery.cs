using CreditProposal.Application.ViewModels;
using MediatR;

namespace CreditProposal.Application.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery: IRequest<IEnumerable<CustomerViewModel>>
    {
    }
}