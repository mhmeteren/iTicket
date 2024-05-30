using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class BusStation : EntityBase
    {
        public string Name { get; set; }
        public string City { get; set; }

        public ICollection<BusRoute> BusRoutes { get; set; }
    }
}
