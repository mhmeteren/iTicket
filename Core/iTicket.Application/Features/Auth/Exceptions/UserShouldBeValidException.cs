using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class UserShouldBeValidException() : BaseBadRequestException("Invalid User")
    {
    }
}
