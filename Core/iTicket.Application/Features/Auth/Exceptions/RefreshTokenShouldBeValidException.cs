using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShouldBeValidException() : BaseBadRequestException("Invalid refresh token.")
    {
    }

    
}
