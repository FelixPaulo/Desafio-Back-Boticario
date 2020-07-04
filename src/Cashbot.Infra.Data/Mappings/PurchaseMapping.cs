using Cashbot.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashbot.Infra.Data.Mappings
{
    public class PurchaseMapping : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder
                .ToTable("Purchases")
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasColumnName("Id");

            builder
                .Property(c => c.Code)
                .HasColumnName("Code")
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(c => c.Value)
                .HasColumnName("Value")
                .IsRequired();

            builder
              .Property(c => c.Status)
              .HasColumnName("Status")
              .HasMaxLength(50)
              .IsRequired();

            builder
               .Property(c => c.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder
               .HasOne(p => p.Dealer)
               .WithMany()
               .HasForeignKey(p => p.DealerId);
        }
    }
}
