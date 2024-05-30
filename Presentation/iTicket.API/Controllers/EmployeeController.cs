using iTicket.Application.Exceptions;
using iTicket.Application.Features.Employees.Command.CreateEmployee;
using iTicket.Application.Features.Employees.Command.SoftDeleteEmployee;
using iTicket.Application.Features.Employees.Command.UpdateEmployee;
using iTicket.Application.Features.Employees.Queries.GetAllEmployeesByPaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;



        [HttpGet]
        [ProducesResponseType<IList<GetAllEmployeesByPagingQueryResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByCompanyAndPaging([FromQuery] GetAllEmployeesByPagingQueryRequest request)
        {
            IList<GetAllEmployeesByPagingQueryResponse> employees = await mediator.Send(request);
            return Ok(employees);
        }


        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(201);
        }


        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SoftDeleteEmployee([FromBody] SoftDeleteEmployeeCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
