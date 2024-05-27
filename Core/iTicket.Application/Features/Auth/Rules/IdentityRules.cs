using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Auth.Rules
{
    public class IdentityRules : BaseRules
    {
        public Task IdentityResultValidation(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new IdentityResultException(result);
            return Task.CompletedTask;
        }
    }
}
