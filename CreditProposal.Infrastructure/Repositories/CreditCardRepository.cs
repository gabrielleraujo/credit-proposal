using CreditProposal.Domain.Models.Entities;
using CreditProposal.Domain.Repositories;
using CreditProposal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CreditProposal.Infrastructure.Repositories;
public class CreditProposalRepository : ICreditProposalRepository
{
    protected CreditProposalContext _context;
    public CreditProposalRepository(CreditProposalContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CreditProposalEntity customer)
    {
        await _context.CreditProposals.AddAsync(customer);
    }

    public void Update(CreditProposalEntity customer)
    {
        _context.CreditProposals.Update(customer);
    }

    public void Delete(Guid customerId)
    {
        var customer = _context.CreditProposals.First(x => x.Id == customerId);
        _context.CreditProposals.Attach(customer);
        _context.CreditProposals.Remove(customer);
    }

    public async Task AddCustomerAsync(CustomerEntity customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    #region consulting
    public async Task<CreditProposalEntity> FindByAsync(Func<CreditProposalEntity, bool> predicate)
    {
        var response = _context.CreditProposals
            .FirstOrDefault(predicate);

        return await Task.FromResult(response!);
    }

    public async Task<List<CreditProposalEntity>> GetAllAsync()
    {
        return await _context.CreditProposals
            .ToListAsync();
    }


    public async Task<CustomerEntity> FindCustomerByAsync(Func<CustomerEntity, bool> predicate)
    {
        var response = _context.Customers
            .FirstOrDefault(predicate);

        return await Task.FromResult(response!);
    }

    public async Task<List<CustomerEntity>> GetAllCustomersAsync()
    {
        return await _context.Customers
            .ToListAsync();
    }

    
    public async Task<List<CreditProposalEntity>> GetAllCreditProposalsByCustomerId(Guid customerId)
    {
        return await _context.CreditProposals
            .Where(c => c.Customer.Id == customerId)
            .ToListAsync();
    }
    #endregion

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
