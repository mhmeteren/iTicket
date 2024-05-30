using iTicket.Application.Bases;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPaging
{
    public class GetAllBusStationsByPagingQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<GetAllBusStationsByPagingQueryRequest, IList<GetAllBusStationsByPagingQueryResponse>>
    {
        public async Task<IList<GetAllBusStationsByPagingQueryResponse>> Handle(GetAllBusStationsByPagingQueryRequest request, CancellationToken cancellationToken)
        {
            IList<BusStation> busStations = await unitOfWork
                 .GetReadRepository<BusStation>()
                 .GetAllByPagingAsync(
                 currentPage: request.CurrentPage,
                 pageSize: request.PageSize,
                 predicate: x => !x.IsDeleted && x.City.StartsWith(request.City));

            return mapper.Map<GetAllBusStationsByPagingQueryResponse, BusStation>(busStations);
        }
    }
}
