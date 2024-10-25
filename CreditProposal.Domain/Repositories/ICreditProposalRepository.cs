using CreditProposal.Domain.Models.Entities;

namespace CreditProposal.Domain.Repositories;

public interface ICreditProposalRepository
{
    #region commands
    Task AddAsync(CreditProposalEntity customerEntity);
    void Update(CreditProposalEntity customerEntity);
    void Delete(Guid CustomerEntityId);

    Task AddCustomerAsync(CustomerEntity customerEntity);

    #endregion
    
    #region queries
    Task<CreditProposalEntity> FindByAsync(Func<CreditProposalEntity, bool> predicate);
    Task<List<CreditProposalEntity>> GetAllAsync();

    Task<CustomerEntity> FindCustomerByAsync(Func<CustomerEntity, bool> predicate);
    Task<List<CustomerEntity>> GetAllCustomersAsync();
    Task<List<CreditProposalEntity>> GetAllCreditProposalsByCustomerId(Guid customerId);
    #endregion

    Task CommitAsync();
    void Dispose();
}
