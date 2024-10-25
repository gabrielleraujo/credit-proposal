using CreditProposal.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditProposal.Infrastructure.Persistence.Configurations.Entities;

public class CreditProposalConfiguration : IEntityTypeConfiguration<CreditProposalEntity>
{
    public void Configure(EntityTypeBuilder<CreditProposalEntity> builder)
    {
        builder.ToTable("CreditProposal");

        builder.ConfigureBaseEntity();
        
        builder.Property(x => x.IsCreditReleased)
            .HasColumnName("IsCreditReleased");
                
        builder.Property(x => x.CreditLimitReleased)
            .HasColumnName("CreditLimitReleased");

        // Configurando a relação sem expor CustomerId diretamente
        builder
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId") // Use a chave estrangeira nomeada "CustomerId" que será gerada
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex("CustomerId");
    }
}
