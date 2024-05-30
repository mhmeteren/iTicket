using iTicket.Application.Bases;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPagingForAdmin
{
    public class GetAllBusStationsByPagingForAdminQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<GetAllBusStationsByPagingForAdminQueryRequest, IList<GetAllBusStationsByPagingForAdminQueryResponse>>

    {
        public async Task<IList<GetAllBusStationsByPagingForAdminQueryResponse>> Handle(GetAllBusStationsByPagingForAdminQueryRequest request, CancellationToken cancellationToken)
        {
           IList<BusStation> busStations = await unitOfWork
                .GetReadRepository<BusStation>()
                .GetAllByPagingAsync(
                currentPage: request.CurrentPage,
                pageSize: request.PageSize,
                predicate: x => x.IsDeleted == request.IsDeleted);

            return mapper.Map<GetAllBusStationsByPagingForAdminQueryResponse, BusStation>(busStations);
        }
    }
}
