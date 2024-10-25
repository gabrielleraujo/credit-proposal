using CreditProposal.Application.Commands.AvaliateCreditProposalCommand;
using CreditProposal.Application.Validations.Utils;
using FluentValidation;

namespace CreditProposal.Application.Validations;

public class AvaliateCreditProposalCommandValidation : AbstractValidator<AvaliateCreditProposalCommand>
{
    public AvaliateCreditProposalCommandValidation()
    {
        RuleFor(c => c.Event.CustomerId).ValidateNullOrEmpty("CustomerId");
        RuleFor(c => c.Event.Email).ValidateNullOrEmpty("Email");
        RuleFor(c => c.Event.Address).ValidateNullOrEmpty("Email");

        RuleFor(c => c.Event.Address.PostalCode).ValidateNullOrEmpty("PostalCode");
        RuleFor(c => c.Event.Address.State).ValidateNullOrEmpty("State");
        RuleFor(c => c.Event.Address.City).ValidateNullOrEmpty("City");
        RuleFor(c => c.Event.Address.Neighborhood).ValidateNullOrEmpty("Neighborhood");
        RuleFor(c => c.Event.Address.Street).ValidateNullOrEmpty("Street");
        RuleFor(c => c.Event.Address.Complement).ValidateNullOrEmpty("Complement");
    }
}
