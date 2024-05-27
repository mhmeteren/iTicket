using iTicket.Application.Bases;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class EmailAddressShouldBeValidException() : BaseBadRequestException("Email address invalid")
    {
    }
}
