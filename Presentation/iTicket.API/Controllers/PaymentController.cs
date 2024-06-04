using iTicket.Application.Exceptions;
using iTicket.Application.Features.BusSeats.Command.CreateBusSeat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="user")]
    public class PaymentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuyTickets([FromBody] CreateBusSeatCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(201);
        }
        
    }
}
