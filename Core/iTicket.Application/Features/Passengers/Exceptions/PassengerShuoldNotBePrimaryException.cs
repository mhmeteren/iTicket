using iTicket.Application.Bases;

namespace iTicket.Application.Features.Passengers.Exceptions
{
    public class PassengerShuoldNotBePrimaryException() : BaseBadRequestException("Primary passenger cannot be deleted") { }
}
