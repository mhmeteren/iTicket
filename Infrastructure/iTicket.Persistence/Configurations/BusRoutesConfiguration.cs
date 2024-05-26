using iTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTicket.Persistence.Configurations
{
    public class BusRoutesConfiguration : IEntityTypeConfiguration<BusRoute>
    {
        public void Configure(EntityTypeBuilder<BusRoute> builder)
        {
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Date).IsRequired();

            builder.HasOne(x=> x.BusStation)
                .WithMany(x => x.BusRoutes)
                .HasForeignKey(x=>x.BusStationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.BusSchedule)
                .WithMany(x => x.BusRoutes)
                .HasForeignKey(x =>x.BusScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
