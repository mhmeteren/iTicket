using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShoulNotBeExpiredException() : BaseBadRequestException("Session has expired")
    {
    }

    
}
