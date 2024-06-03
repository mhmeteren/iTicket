using iTicket.Application.Bases;
using MediatR;

namespace iTicket.Application.Features.BusSchedules.Queries.GetAllBusSchedulesByRoutesAndPaging
{
    public class GetAllBusSchedulesByRoutesAndPagingQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllBusSchedulesByRoutesAndPagingQueryResponse>>
    {
        public int StartBusStationId { get; set; } = 0;
        public int EndBusStationId { get; set; } = 0;
        public int TripType { get; set; } = (int)Common.Enums.TripType.TwoPlusOne;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
