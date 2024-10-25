using CreditProposal.Application.ViewModels;
using CreditProposal.Domain.Repositories;
using MediatR;

namespace CreditProposal.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>
    {
        private readonly ICreditProposalRepository _repository;

        public GetCustomerByIdQueryHandler(
            ICreditProposalRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerViewModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindCustomerByAsync(x => x.Id == request.CustomerId);

            if (entity == null) return null;

            var viewModel = CustomerViewModel.MapFromDomain(entity);

            return viewModel;
        }
    }
}
