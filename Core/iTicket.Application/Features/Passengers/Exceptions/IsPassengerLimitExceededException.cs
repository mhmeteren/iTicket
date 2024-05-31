using iTicket.Application.Bases;

namespace iTicket.Application.Features.Passengers.Exceptions
{
    public class IsPassengerLimitExceededException() : BaseBadRequestException("Passenger Limit Exceeded")
    {
    }
}
