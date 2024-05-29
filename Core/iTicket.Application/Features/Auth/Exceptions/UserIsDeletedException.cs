using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class UserIsDeletedException() : BaseBadRequestException("user account is invalid")
    {

    }
}
