using iTicket.Application.Exceptions;
using iTicket.Application.Features.BusRoutes.Command.SoftDeleteBusRoute;
using iTicket.Application.Features.BusSchedules.Command.CreateBusSchedule;
using iTicket.Application.Features.BusSchedules.Command.SoftDeleteBusSchedule;
using iTicket.Application.Features.BusSchedules.Command.UpdateBusSchedule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BusScheduleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;


        [HttpPost]
        [Authorize(Roles ="employee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateBusSchedule([FromBody] CreateBusScheduleCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(201);
        }


        [HttpPut]
        [Authorize(Roles = "employee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateBusSchedule([FromBody] UpdateBusScheduleCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "employee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SoftDeleteBusSchedule([FromBody] SoftDeleteBusScheduleCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpPut]
        [Authorize(Roles = "employee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SoftDeleteBusRoute([FromBody] SoftDeleteBusRouteCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
