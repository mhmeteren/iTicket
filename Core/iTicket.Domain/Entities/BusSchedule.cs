using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class BusSchedule : EntityBase
    {
        public int TripType { get; set; }
        public int SeatCount { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<BusRoute> BusRoutes { get; set; }
        public ICollection<BusSeat> BusSeats { get; set; }


    }

}
