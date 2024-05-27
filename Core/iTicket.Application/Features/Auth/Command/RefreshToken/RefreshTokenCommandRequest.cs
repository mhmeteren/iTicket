using MediatR;

namespace iTicket.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
