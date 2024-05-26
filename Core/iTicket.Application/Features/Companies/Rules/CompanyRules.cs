using iTicket.Application.Bases;
using iTicket.Application.Features.Companies.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.Companies.Rules
{
    public class CompanyRules : BaseRules
    {

        public Task CompanyTitleMustBeUnique(string requestName, Company company)
        {
            if (company != null && company.Name.Equals(requestName)) throw new CompanyTitleMustBeUniqueException();
            return Task.CompletedTask;
        }

    }
}
