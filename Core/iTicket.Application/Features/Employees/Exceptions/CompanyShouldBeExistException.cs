using iTicket.Application.Bases;

namespace iTicket.Application.Features.Employees.Exceptions
{
    public class CompanyShouldBeExistException() : BaseNotFoundException("Company not found")
    {
    }
}
