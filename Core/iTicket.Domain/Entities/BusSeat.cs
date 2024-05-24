using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class BusSeat : EntityBase
    {
        public int Status { get; set; }
        public int SeatNumber { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public int BusScheduleId { get; set; }
        public BusSchedule BusSchedule { get; set; }
    }

}
