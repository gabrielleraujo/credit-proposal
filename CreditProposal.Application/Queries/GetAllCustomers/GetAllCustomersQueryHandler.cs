using CreditProposal.Application.Queries.GetAllCustomers;
using CreditProposal.Application.ViewModels;
using CreditProposal.Domain.Repositories;
using MediatR;

namespace CreditProposal.Application.Queries.GetAllCreditProposalsQ
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerViewModel>>
    {
        private readonly ICreditProposalRepository _repository;

        public GetAllCustomersQueryHandler(
            ICreditProposalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerViewModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllCustomersAsync();

            if (entity == null) return new List<CustomerViewModel>();

            var viewModel = CustomerViewModel.MapFromDomain(entity);

            return viewModel;
        }
    }
}
