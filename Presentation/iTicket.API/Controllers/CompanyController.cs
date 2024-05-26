﻿using iTicket.Application.Features.Companies.Queries.GetAllCompaniesByPaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllByPaging([FromQuery] GetAllCompaniesByPagingQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

    }
}