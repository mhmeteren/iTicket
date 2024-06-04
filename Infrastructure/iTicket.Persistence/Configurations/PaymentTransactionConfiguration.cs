using iTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTicket.Persistence.Configurations
{
    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.Property(x => x.PaymentId).IsRequired();
            builder.Property(x => x.PaymentTransactionId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.PaidPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.ConvertedPayoutPaidPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.MerchantPayoutAmount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.TransactionLastUpdateDate).IsRequired(false);

            builder.HasOne(x => x.BusSeat)
                .WithMany(x => x.PaymentTransactions)
                .HasForeignKey(x => x.BusSeatId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
