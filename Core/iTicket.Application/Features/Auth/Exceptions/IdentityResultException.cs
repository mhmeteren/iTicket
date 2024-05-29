using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Auth.Exceptions
{
    public class IdentityResultException(IdentityResult identityResult) : Exception
    {
        public readonly IEnumerable<string> Errors = identityResult.Errors.Select(e => e.Description);

    }
}
