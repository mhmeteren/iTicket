using iTicket.Application.Bases;
using iTicket.Application.Features.Passengers.Command.CreatePassenger;
using iTicket.Application.Features.Passengers.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Passengers.Queries.GetAllPassengersByPaging
{
    public class GetAllPassengersByPagingQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllPassengersByPagingQueryResponse>>
    {
    }
}
