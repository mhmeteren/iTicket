using iTicket.Application.Bases;
using MediatR;

namespace iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPaging
{
    public class GetAllBusStationsByPagingQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllBusStationsByPagingQueryResponse>>
    {
        public string City { get; set; }
    }
}
