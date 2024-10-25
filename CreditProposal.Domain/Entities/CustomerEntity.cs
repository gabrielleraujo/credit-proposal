using CreditProposal.Domain.Models.Abstracts;
using CreditProposal.Domain.Models.ValueObject;

namespace CreditProposal.Domain.Models.Entities;

public class CustomerEntity : BaseEntity
{
    private CustomerEntity() {}
    public CustomerEntity(
        Guid id,
        Name name, 
        Email mainEmail,
        Address address
        ) : base(id)
    {
        Name = name;
        MainEmail = mainEmail;
        Address = address;
        Validate();
    }

    public Name Name { get; private set; }

    public Email MainEmail { get; private set; }
    public Address Address { get; set; }

    protected override void ApplyValidation()
    {
        if (Id == null)
        {
            AddError("The Id cannot be null.");
        }
    }
}
