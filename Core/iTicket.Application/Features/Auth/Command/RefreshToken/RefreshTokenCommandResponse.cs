namespace iTicket.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
