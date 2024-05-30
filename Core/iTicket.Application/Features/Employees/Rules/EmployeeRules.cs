using iTicket.Application.Bases;
using iTicket.Application.Features.Employees.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.Employees.Rules
{
    public class EmployeeRules : BaseRules
    {

        public Task CompanyShouldBeExist(Company? company)
        {
            if (company is null || company.IsDeleted) throw new CompanyShouldBeExistException();

            return Task.CompletedTask;
        }

    }
}
