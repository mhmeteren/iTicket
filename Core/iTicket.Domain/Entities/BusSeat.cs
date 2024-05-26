using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class BusSeat : EntityBase
    {
        public int Status { get; set; }
        public int SeatNumber { get; set; }
        public int Price { get; set; }


        public int StartBusRouteId { get; set; }
        public BusRoute StartBusRoute { get; set; }

        public int EndBusRouteId { get; set; }
        public BusRoute EndBusRoute { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public int BusScheduleId { get; set; }
        public BusSchedule BusSchedule { get; set; }
    }

}
