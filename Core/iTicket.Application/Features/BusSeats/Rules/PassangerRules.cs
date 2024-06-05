using iTicket.Application.Bases;
using iTicket.Application.Features.BusSeats.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.BusSeats.Rules
{
    public class PassangerRules : BaseRules
    {
        public Task PrimaryPassangerShouldBeExists(Passenger? passenger)
        {
            if (passenger is null || passenger.IsDeleted) throw new PrimaryPassangerShouldBeExistsExcecption();
            return Task.CompletedTask;
        }
    }
}
