﻿using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class Employee : BaseUser, IEntityBase
    {

        public string FullName { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<BusSchedule> BusSchedules { get; set; }
    }
}
