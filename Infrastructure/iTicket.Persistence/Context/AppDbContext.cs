using iTicket.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace iTicket.Persistence.Context
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<BaseUser, Role, Guid>(options)
    {

       // public DbSet<User> User { get; set; }
        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }
        public DbSet<BusSeat> BusSeats { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Passenger> Passengers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
