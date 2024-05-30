using iTicket.Application.Bases;
using iTicket.Application.Features.Companies.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.Companies.Rules
{
    public class CompanyRules : BaseRules
    {

        public Task CompanyNameMustBeUnique(Company company)
        {
            if (company != null) throw new CompanyTitleMustBeUniqueException();
            return Task.CompletedTask;
        }

        public Task CompanyIsDeleted(Company? company)
        {
            if (company is null || company.IsDeleted) throw new CompanyIsDeletedException();
            return Task.CompletedTask;
        }


        public Task CompanyNameShouldNotBeExistIgnoreOwn(Company? companyCheck, int requestId)
        {
            if (companyCheck != null && companyCheck.Id != requestId) throw new CompanyTitleMustBeUniqueException();
            return Task.CompletedTask;
        }
    }
}
