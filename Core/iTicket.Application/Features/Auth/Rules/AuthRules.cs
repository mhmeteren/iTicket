using iTicket.Application.Bases;
using iTicket.Application.Features.Auth.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.Auth.Rules
{
    public class AuthRules : BaseRules
    {


        public Task UserIsDeleted(BaseUser? user)
        {
            if (user is null || user.IsDeleted) throw new UserIsDeletedException();
            return Task.CompletedTask;
        }

        public Task UserShouldBeConfirmed(BaseUser user)
        {
            if (!user.EmailConfirmed) throw new UserShouldBeConfirmedException();
            return Task.CompletedTask;
        }

        public Task UserShouldNotBeExistIgnoreOwn(BaseUser? user, Guid requestId)
        {
            if (user is not null && !user.Id.Equals(requestId)) throw new UserAlreadyExistsException();
            return Task.CompletedTask;
        }

        public Task UserShouldNotBeExist(BaseUser? user)
        {
            if (user is not null) throw new UserAlreadyExistsException();
            return Task.CompletedTask;
        }


        public Task UserRoleShouldBeEquals(string userRole, string requestRole)
        {
            if (userRole is null || !userRole.Equals(requestRole)) throw new UserShouldBeExistException();
            return Task.CompletedTask;
        }

        public Task EmailOrPasswordShouldNotBeInvalid(bool checkPassword)
        {
            if (!checkPassword) throw new EmailOrPasswordShouldNotBeInvalidException();
            return Task.CompletedTask;
        }

        public Task RefreshTokenShouldBeValid(string? userRefreshToken, string requestRefreshToken)
        {
            if (userRefreshToken is null || !userRefreshToken.Equals(requestRefreshToken))
                throw new RefreshTokenShouldBeValidException();

            return Task.CompletedTask;
        }

        public Task RefreshTokenShoulNotBeExpired(DateTime? refreshTokenExpireTime)
        {
            if (refreshTokenExpireTime is not null && refreshTokenExpireTime <= DateTime.Now)
                throw new RefreshTokenShoulNotBeExpiredException();
            return Task.CompletedTask;
        }


        public Task SessionAlreadyRevoked(BaseUser user)
        {
            if (user.RefreshToken is null || user.RefreshTokenExpireTime is null)
                throw new SessionAlreadyRevokedException();
            return Task.CompletedTask;
        }
    }
}
