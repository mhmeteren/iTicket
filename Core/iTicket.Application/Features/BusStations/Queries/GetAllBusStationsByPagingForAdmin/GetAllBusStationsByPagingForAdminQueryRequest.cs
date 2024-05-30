using iTicket.Application.Bases;
using MediatR;

namespace iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPagingForAdmin
{
    public class GetAllBusStationsByPagingForAdminQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllBusStationsByPagingForAdminQueryResponse>>
    {

        public bool IsDeleted { get; set; } = false;
    }
}
