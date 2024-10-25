using System.Text.Json.Serialization;
using CreditProposal.Domain.Models.Entities;

namespace CreditProposal.Application.ViewModels;

public record CreditProposalViewModel : BaseEntityViewModel
{
    public CreditProposalViewModel(
        Guid id, DateTime createAt, DateTime? lastUpdate,
        Guid customerId,
        bool isCreditReleased, 
        decimal creditLimitReleased,
        string address)
        : base(id, createAt, lastUpdate)
    {
        CustomerId = customerId;
        IsCreditReleased = isCreditReleased;
        CreditLimitReleased = creditLimitReleased;
        Address = address;
    }

    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; private set; }

    [JsonPropertyName("is-credit-released")]
    public bool IsCreditReleased { get; private set; } 

    [JsonPropertyName("credit-limit-released")]
    public decimal CreditLimitReleased { get; private set; }

    [JsonPropertyName("address")]
    public string Address { get; private set; }

    public static CreditProposalViewModel MapFromDomain(CreditProposalEntity entity) => 
        new CreditProposalViewModel(
            entity.Id,
            entity.CreateAt,
            entity.LastUpdate,
            entity.Customer.Id,
            entity.IsCreditReleased,
            entity.CreditLimitReleased,
            entity.Customer.Address.ToString()
        );

    public static IEnumerable<CreditProposalViewModel> MapFromDomain(IList<CreditProposalEntity> entity)
    {
        foreach (var item in entity)
        {
            yield return MapFromDomain(item);
        }
    }
}
