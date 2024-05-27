using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.Auth.Rules
{
    public class AuthRules : BaseRules
    {
        public Task UserShouldNotBeExist(BaseUser? user)
        {
            if (user is not null) throw new UserAlreadyExistsException();
            return Task.CompletedTask;
        }

        public Task EmailOrPasswordShouldNotBeInvalid(BaseUser? user, bool checkPassword)
        {
            if (user is null || !checkPassword) throw new EmailOrPasswordShouldNotBeInvalidException();
            return Task.CompletedTask;
        }

        public Task RefreshTokenShoulNotBeExpired(DateTime? refreshTokenExpireTime)
        {
            if (refreshTokenExpireTime is not null && refreshTokenExpireTime <= DateTime.Now)
                throw new RefreshTokenShoulNotBeExpiredException();
            return Task.CompletedTask;
        }

        public Task EmailAddressShouldBeValid(BaseUser? user)
        {
            if (user is null) throw new EmailAddressShouldBeValidException();
            return Task.CompletedTask;
        }
    }
}
