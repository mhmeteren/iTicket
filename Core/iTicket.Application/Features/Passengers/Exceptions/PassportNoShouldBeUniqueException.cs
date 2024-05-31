using iTicket.Application.Bases;

namespace iTicket.Application.Features.Passengers.Exceptions
{
    public class PassportNoShouldBeUniqueException() : BaseBadRequestException("Passport No Should be Unique")
    {
    }
}
