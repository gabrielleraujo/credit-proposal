using System.Text.RegularExpressions;
using CreditProposal.Domain.Models.Abstracts;

namespace CreditProposal.Domain.Models.ValueObject;

public class Email : BaseDomainModel
{
    public Email(string text)
    {
        Text = text;
        Validate();
    }

    public string Text { get; private set; } = string.Empty;

    protected override void ApplyValidation()
    {
        var result = Regex.Match(Text, "^\\S+@\\S+\\.\\S+$");

        if (!result.Success)
        {
            AddError("The e-mail is invalid.");
        }
    }
}
