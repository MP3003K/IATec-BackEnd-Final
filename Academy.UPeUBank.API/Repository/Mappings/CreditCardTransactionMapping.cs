using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class CreditCardTransactionMapping : IEntityTypeConfiguration<CreditCardTransaction>
    {
        public void Configure(EntityTypeBuilder<CreditCardTransaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CreditCard)
                .WithMany(x => x.CreditCardTransactions)
                .HasForeignKey(x => x.CreditCardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Date)
            .HasDefaultValueSql("GETDATE()");

            builder.ToTable("CreditCardTransaction");
        }
    }
}
