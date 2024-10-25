using CreditProposal.Application.Queries.GetAllCustomers;
using CreditProposal.Application.ViewModels;
using CreditProposal.Domain.Repositories;
using MediatR;

namespace CreditProposal.Application.Queries.GetAllCreditProposalsQ
{
    public class GetCreditProposalsByCustomerIdQueryHandler : IRequestHandler<GetCreditProposalsByCustomerIdQuery, IEnumerable<CreditProposalViewModel>>
    {
        private readonly ICreditProposalRepository _repository;

        public GetCreditProposalsByCustomerIdQueryHandler(
            ICreditProposalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CreditProposalViewModel>> Handle(GetCreditProposalsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllCreditProposalsByCustomerId(request.CustomerId);

            if (entity == null) return new List<CreditProposalViewModel>();

            var viewModel = CreditProposalViewModel.MapFromDomain(entity);

            return viewModel;
        }
    }
}
