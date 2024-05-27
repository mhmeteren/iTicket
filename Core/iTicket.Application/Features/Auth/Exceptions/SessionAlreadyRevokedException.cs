using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class SessionAlreadyRevokedException() : BaseBadRequestException("Session already revoked")
    {
    }
}
