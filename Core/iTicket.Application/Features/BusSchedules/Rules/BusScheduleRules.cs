using iTicket.Application.Bases;
using iTicket.Application.Features.BusSchedules.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.BusSchedules.Rules
{
    public class BusScheduleRules : BaseRules
    {
        public Task OwnerEmployeeIsDeleted(Employee? owner)
        {
            if (owner is null || owner.IsDeleted) throw new OwnerEmployeeIsDeletedException();
            return Task.CompletedTask;
        }

        public Task BusScheduleIsDeleted(BusSchedule? busSchedule)
        {
            if (busSchedule is null || busSchedule.IsDeleted) throw new BusScheduleIsDeletedException();
            return Task.CompletedTask;
        }

        public Task BusScheduleShouldNotHaveBusSeat(int busSeatsCount)
        {
            if (busSeatsCount > 0) throw new BusScheduleShouldNotHaveBusSeatException();
            return Task.CompletedTask;
        }
    }
}
