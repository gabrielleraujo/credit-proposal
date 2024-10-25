using CreditProposal.Domain.Models.Entities;
using CreditProposal.Domain.Models.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditProposal.Infrastructure.Persistence.Configurations.Entities;

public class CustomerConfiguration: IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.ToTable("Customers");

        builder.ConfigureBaseEntity();

        builder.OwnsOne(x => x.Name, y =>
            {
                y.Property(y => y.First)
                    .HasColumnName("FirstName")
                    .IsRequired();

                y.Property(y => y.Last)
                    .HasColumnName("LastName")
                    .IsRequired();
            });

        builder.Property(x => x.MainEmail)
            .HasColumnName("MainEmail")
                .HasConversion(
                    x => x.Text,
                    text => new Email(text)
                ).IsRequired();

        builder.OwnsOne(x => x.Address, y =>
        {
            y.Property(x => x.PostalCode)
            .HasColumnName("PostalCode");
        
            y.Property(x => x.State)
                .HasColumnName("State");

            y.Property(x => x.City)
                .HasColumnName("City");

            y.Property(x => x.Neighborhood)
                .HasColumnName("Neighborhood");

            y.Property(x => x.Street)
                .HasColumnName("Street");

            y.Property(x => x.Number)
                .HasColumnName("Number");

            y.Property(x => x.Complement)
                .HasColumnName("Complement");
        });
    }
}
