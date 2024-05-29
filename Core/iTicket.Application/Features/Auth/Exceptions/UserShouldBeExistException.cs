using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class UserShouldBeExistException() : BaseNotFoundException("user account not found.")
    {
    }
}
