using iTicket.Application.Exceptions;
using iTicket.Application.Features.BusStations.Command.CreateBusStation;
using iTicket.Application.Features.BusStations.Command.SoftDeleteBusStation;
using iTicket.Application.Features.BusStations.Command.UpdateBusStation;
using iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPaging;
using iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPagingForAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BusStationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;



        [HttpGet]
        [ProducesResponseType<IList<GetAllBusStationsByPagingQueryResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByPaging([FromQuery] GetAllBusStationsByPagingQueryRequest request)
        {
            IList<GetAllBusStationsByPagingQueryResponse> busStations = await mediator.Send(request);
            return Ok(busStations);
        }



        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType<IList<GetAllBusStationsByPagingForAdminQueryResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByPagingForAdmin([FromQuery] GetAllBusStationsByPagingForAdminQueryRequest request)
        {
            IList<GetAllBusStationsByPagingForAdminQueryResponse> busStations = await mediator.Send(request);
            return Ok(busStations);
        }


        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateBusStation([FromBody]CreateBusStationCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(201);
        }



        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateBusStation([FromBody] UpdateBusStationCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SoftDeleteBusStation([FromBody] SoftDeleteBusStationCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
