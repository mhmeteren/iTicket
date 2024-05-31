using iTicket.Application.Bases;

namespace iTicket.Application.Features.Passengers.Exceptions
{
    public class IdentificationNoShouldBeUniqueException() : BaseBadRequestException("Identification No Should be Unique")
    {
    }
}
