
using iTicket.Application.Bases;

namespace iTicket.Application.Features.Companies.Exceptions
{
    public class CompanyIsDeletedException() : BaseBadRequestException("Comany is deleted.")
    {
    }
}
