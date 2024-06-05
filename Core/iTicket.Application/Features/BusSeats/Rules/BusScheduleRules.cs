using iTicket.Application.Bases;
using iTicket.Application.Features.BusSeats.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.BusSeats.Rules
{
    public class BusScheduleRules : BaseRules
    {
        public Task BusScheduleShouldBeExits(BusSchedule? busSchedule)
        {
            if (busSchedule is null || busSchedule.IsDeleted) throw new BusScheduleShouldBeExitsException();
            return Task.CompletedTask;
        }
    }
}
