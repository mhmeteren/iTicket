using iTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTicket.Persistence.Configurations
{
    public class BusScheduleConfiguration : IEntityTypeConfiguration<BusSchedule>
    {
        public void Configure(EntityTypeBuilder<BusSchedule> builder)
        {
            builder.Property(x => x.TripType).IsRequired();
            builder.Property(x => x.SeatCount).IsRequired();


            builder.HasOne(x=> x.Company)
                .WithMany(x => x.BusSchedules)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.BusSchedules)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
