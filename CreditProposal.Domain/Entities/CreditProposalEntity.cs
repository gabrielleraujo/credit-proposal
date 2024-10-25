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

    public Guid CustomerId { get; private set; } // Propriedade que representa a chave estrangeira, mas não é pública
    public CustomerEntity Customer { get; private set; }
    public bool IsCreditReleased { get; set; }
    public decimal CreditLimitReleased { get; set; }
    //public string CreditOfferName { get; private set; } = string.Empty;
    //public string Description { get; private set; } = string.Empty;

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

    // protected override void ChooseCreditCard()
    // {
    //     if (CreditLimitReleased <= 5000m)
    //     {
    //         IsActive = true;
    //         CreditOfferName = "Standard";
    //         Flag = "Visa";
    //         Description = "Cartão de crédito inicial.";
    //     }
    //     if (CreditLimitReleased > 5000m)
    //     {
    //         IsActive = true;
    //         CreditOfferName = "Platinum";
    //         Flag = "Visa";
    //         Description = "Cartão de crédito intermediário.";
    //     }
    // }
}
