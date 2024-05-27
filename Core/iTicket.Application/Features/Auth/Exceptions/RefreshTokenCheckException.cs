using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class RefreshTokenCheckException() : BaseBadRequestException("Invalid refresh token.")
    {
    }

    
}
