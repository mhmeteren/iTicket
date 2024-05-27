using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBeInvalidException() : BaseBadRequestException("Email or Password is Invalid")
    {
    }
}
