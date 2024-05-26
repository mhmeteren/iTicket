
using iTicket.Application.Bases;

namespace iTicket.Application.Features.Companies.Exceptions
{
    public class CompanyTitleMustBeUniqueException() : BaseBadRequestException("Comany title must be unique.")
    {
    }
}
