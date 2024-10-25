using CreditProposal.Domain.Models.Entities;
using CreditProposal.Infrastructure.Persistence.Configurations.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditProposal.Infrastructure.Persistence;

public class CreditProposalContext: DbContext
{
    public CreditProposalContext(DbContextOptions options) : base(options) { }
    
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<CreditProposalEntity> CreditProposals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CreditProposalConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}