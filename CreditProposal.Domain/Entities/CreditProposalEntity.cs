using CreditProposal.Domain.Models.Abstracts;

namespace CreditProposal.Domain.Models.Entities;

public class CreditProposalEntity : BaseEntity
{
    private CreditProposalEntity() {}
    public CreditProposalEntity(
        Guid id,
        CustomerEntity customer) : base(id)
    {
        Customer = customer;
        Validate();
        CustomerId = customer.Id;
    }

    public Guid CustomerId { get; private set; }
    public CustomerEntity Customer { get; private set; }
    public bool IsCreditReleased { get; private set; }
    public decimal CreditLimitReleased { get; private set; }

    protected override void ApplyValidation()
    {
        if (Customer == null) AddError("The Customer cannot be null.");
        if (Customer!.Id == Guid.Empty) AddError("The Customer.Id cannot be null.");
    }

    public void AvaliateCreditOffer()
    {
        if (Customer.Address.State == "RJ")
        {
            IsCreditReleased = true;
            CreditLimitReleased = 5000m;
        }
    }
}
