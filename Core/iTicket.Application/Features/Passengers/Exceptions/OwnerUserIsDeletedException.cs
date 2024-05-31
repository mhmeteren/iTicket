using iTicket.Application.Bases;

namespace iTicket.Application.Features.Passengers.Exceptions
{
    public class OwnerUserIsDeletedException() : BaseBadRequestException("Owner user account is invalid")
    {
    }
}
