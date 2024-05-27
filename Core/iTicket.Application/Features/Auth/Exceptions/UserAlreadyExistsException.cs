using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistsException() : BaseBadRequestException("user already exists")
    {
    }
}
