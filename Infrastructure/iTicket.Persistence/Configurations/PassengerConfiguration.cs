using iTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTicket.Persistence.Configurations
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Gender).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.IsTurkishCitizen).HasDefaultValue(true);
            builder.Property(x => x.IdentificationNo).IsRequired(false).HasMaxLength(11);
            builder.Property(x => x.PassportNo).IsRequired(false).HasMaxLength(9);
            builder.Property(x => x.Nationality).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.DateOfBirth).IsRequired();


            builder.HasOne(x => x.User)
                .WithMany(x => x.Passengers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
