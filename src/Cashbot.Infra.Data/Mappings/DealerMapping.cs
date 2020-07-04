using Cashbot.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashbot.Infra.Data.Mappings
{
    public class DealerMapping : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder
                .ToTable("Dealers")
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasColumnName("Id");

            builder
                .Property(c => c.Name)
                .HasColumnName("Name")
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(c => c.Email)
                .HasColumnName("Email")
                .HasMaxLength(150)
                .IsRequired();

            builder
               .Property(c => c.Cpf)
               .HasColumnName("Cpf")
               .HasMaxLength(15)
               .IsRequired();

            builder
             .Property(c => c.Password)
             .HasColumnName("Password")
             .HasMaxLength(200)
             .IsRequired();
        }
    }
}
