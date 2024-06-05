using iTicket.Application.Bases;
using iTicket.Application.Features.BusSeats.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.BusSeats.Rules
{
    public class BusSeatRules : BaseRules
    {
        public Task BusSeatShouldBeExists(BusSeat? busSeat)
        {
            if (busSeat is null || busSeat.IsDeleted) throw new BusSeatShouldBeExistsException();
            return Task.CompletedTask;
        }

        public Task BusSeatStartDateMustNotHaveExpired(BusSeat busSeat)
        {
            BusSeatShouldBeExists(busSeat);

            if (busSeat.StartBusRoute is not null && busSeat.StartBusRoute.Date <= DateTime.Now)
                throw new BusSeatStartDateMustNotHaveExpiredException();
            return Task.CompletedTask;
        }



        public Task PaymentTransactionShouldBeExits(PaymentTransaction? paymentTransaction)
        {
            if(paymentTransaction is null) throw new PaymentTransactionShouldBeExitsException();
            return Task.CompletedTask;
        }
            
    }
}
