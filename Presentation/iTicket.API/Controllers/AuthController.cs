using iTicket.Application.Exceptions;
using iTicket.Application.Features.Auth.Command.Login;
using iTicket.Application.Features.Auth.Command.RefreshToken;
using iTicket.Application.Features.Auth.Command.Register;
using iTicket.Application.Features.Auth.Command.Revoke;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPost]
        [ProducesResponseType<LoginCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            LoginCommandResponse response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }


        [HttpPost]
        [ProducesResponseType<RefreshTokenCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest request)
        {
            RefreshTokenCommandResponse response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }


        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken()
        {
            await mediator.Send(new RevokeCommandRequest());
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
