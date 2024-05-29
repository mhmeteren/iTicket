using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class UserShouldBeConfirmedException() : BaseBadRequestException("user email is not confirmed")
    {

    }
}
