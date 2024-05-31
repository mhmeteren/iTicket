using iTicket.Application.Exceptions;
using iTicket.Application.Features.Passengers.Command.CreatePassenger;
using iTicket.Application.Features.Passengers.Command.SoftDeletePassenger;
using iTicket.Application.Features.Passengers.Command.UpdatePassenger;
using iTicket.Application.Features.Passengers.Queries.GetAllPassengersByPaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PassengerController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        [ProducesResponseType<IList<GetAllPassengersByPagingQueryResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllPassengerByPaging([FromQuery] GetAllPassengersByPagingQueryRequest request)
        {
            IList<GetAllPassengersByPagingQueryResponse> passengers = await mediator.Send(request);
            return Ok(passengers);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePassanger([FromBody] CreatePassengerCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(201);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassenger([FromBody] UpdatePassengerCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SoftDeletePassenger([FromBody] SoftDeletePassengerCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
