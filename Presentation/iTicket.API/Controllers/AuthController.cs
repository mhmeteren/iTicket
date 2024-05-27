using iTicket.Application.Features.Auth.Command.Login;
using iTicket.Application.Features.Auth.Command.RefreshToken;
using iTicket.Application.Features.Auth.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            LoginCommandResponse response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }


        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest request)
        {
            RefreshTokenCommandResponse response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
