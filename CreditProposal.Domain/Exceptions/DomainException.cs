namespace CreditProposal.Domain.Models.Exceptions;

public class DomainException : Exception
{
    public DomainException(string? message) : base(message)
    {
    }
}
