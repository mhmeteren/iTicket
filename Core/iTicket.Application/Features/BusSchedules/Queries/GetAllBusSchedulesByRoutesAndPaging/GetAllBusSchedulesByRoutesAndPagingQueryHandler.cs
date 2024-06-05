using iTicket.Application.Bases;
using iTicket.Application.Dtos.BusRoute;
using iTicket.Application.Dtos.Companies;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace iTicket.Application.Features.BusSchedules.Queries.GetAllBusSchedulesByRoutesAndPaging
{
    public class GetAllBusSchedulesByRoutesAndPagingQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<GetAllBusSchedulesByRoutesAndPagingQueryRequest, IList<GetAllBusSchedulesByRoutesAndPagingQueryResponse>>
    {
        async Task<IList<GetAllBusSchedulesByRoutesAndPagingQueryResponse>> IRequestHandler<GetAllBusSchedulesByRoutesAndPagingQueryRequest, IList<GetAllBusSchedulesByRoutesAndPagingQueryResponse>>.Handle(GetAllBusSchedulesByRoutesAndPagingQueryRequest request, CancellationToken cancellationToken)
        {
            IList<BusSchedule>? busSchedules = await unitOfWork
                .GetReadRepository<BusSchedule>()
                .GetAllByPagingAsync(
                include: x => x.Include(i => i.BusRoutes).Include(i => i.Company),
                predicate: bs => bs.TripType == request.TripType
                && bs.BusRoutes.Any(br => 
                br.BusStationId == request.StartBusStationId 
                && br.Date.Day == request.Date.Day)
                && bs.BusRoutes.Any(br => br.BusStationId == request.EndBusStationId)
                && bs.BusRoutes.Any(br => br.Date > DateTime.Now),

                currentPage: request.CurrentPage,
                pageSize: request.PageSize);

            var route = mapper.Map<BusRouteDtoForBusSchedule, BusRoute>(new BusRoute());
            var company = mapper.Map<CompanyDtoForBusSchedule, Company>(new Company());
            var bs = mapper.Map<GetAllBusSchedulesByRoutesAndPagingQueryResponse, BusSchedule>(busSchedules);
            return bs;
        }
    }
}
