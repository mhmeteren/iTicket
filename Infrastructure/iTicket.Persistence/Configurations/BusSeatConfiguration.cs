using iTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTicket.Persistence.Configurations
{
    public class BusSeatConfiguration : IEntityTypeConfiguration<BusSeat>
    {
        public void Configure(EntityTypeBuilder<BusSeat> builder)
        {
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.SeatNumber).IsRequired();
            builder.Property(x => x.Price).IsRequired();


            builder.HasOne(x => x.StartBusRoute)
                .WithMany()
                .HasForeignKey(x => x.StartBusRouteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.EndBusRoute)
                 .WithMany()
                 .HasForeignKey(x => x.EndBusRouteId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Passenger)
                 .WithMany(x => x.BusSeats)
                 .HasForeignKey(x => x.PassengerId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.BusSchedule)
                 .WithMany(x => x.BusSeats)
                 .HasForeignKey(x => x.BusScheduleId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
