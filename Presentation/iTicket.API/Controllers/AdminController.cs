using iTicket.Application.Exceptions;
using iTicket.Application.Features.Admins.Command.CreateAdmin;
using iTicket.Application.Features.Admins.Command.ResetPasswordAdmin;
using iTicket.Application.Features.Admins.Command.SoftDeleteAdmin;
using iTicket.Application.Features.Admins.Command.UpdateAdmin;
using iTicket.Application.Features.Admins.Queries.GetAllAdminsByPaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;



        [HttpGet]
        [ProducesResponseType<IList<GetAllAdminsByPagingQueryResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAdminByPaging([FromQuery] GetAllAdminsByPagingQueryRequest request)
        {
            IList<GetAllAdminsByPagingQueryResponse> admins = await mediator.Send(request);
            return Ok(admins);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(201);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordAdminCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SoftDelete([FromBody] SoftDeleteAdminCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
