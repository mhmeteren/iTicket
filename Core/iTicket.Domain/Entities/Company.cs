using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class Company : EntityBase
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<BusSchedule> BusSchedules { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
