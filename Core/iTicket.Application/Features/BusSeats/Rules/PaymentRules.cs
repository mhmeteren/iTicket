using iTicket.Application.Bases;
using iTicket.Application.Features.BusSeats.Exceptions;

namespace iTicket.Application.Features.BusSeats.Rules
{
    public class PaymentRules : BaseRules
    {
        public Task PaymentResultStatusShouldNotBeFailure(string status)
        {
            if (status.Equals("failure")) throw new PaymentResultShouldBeSuccessException();
            return Task.CompletedTask;
        }
    }
}
