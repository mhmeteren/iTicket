using iTicket.Application.Exceptions;
using iTicket.Application.Features.Companies.Command.CreateCompany;
using iTicket.Application.Features.Companies.Command.SoftDeleteCompany;
using iTicket.Application.Features.Companies.Command.UpdateCompany;
using iTicket.Application.Features.Companies.Queries.GetAllCompaniesByPaging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        [ProducesResponseType<IList<GetAllCompaniesByPagingQueryResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByPaging([FromQuery] GetAllCompaniesByPagingQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCompany([FromForm] CreateCompanyCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(201);
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompany([FromForm] UpdateCompanyCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ExceptionModel>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SoftDeleteCompany([FromBody] SoftDeleteCompanyCommandRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
