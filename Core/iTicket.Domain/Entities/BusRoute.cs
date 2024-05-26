using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class BusRoute : EntityBase
    {

        public int Price { get; set; }
        public DateTime Date { get; set; }

        public int BusStationId { get; set; }
        public BusStation BusStation { get; set; }

        public int BusScheduleId { get; set; }
        public BusSchedule BusSchedule { get; set; }

    }



}
