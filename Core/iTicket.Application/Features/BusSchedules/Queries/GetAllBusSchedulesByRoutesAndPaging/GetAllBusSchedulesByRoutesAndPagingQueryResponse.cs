using iTicket.Application.Dtos.BusRoute;
using iTicket.Application.Dtos.Companies;

namespace iTicket.Application.Features.BusSchedules.Queries.GetAllBusSchedulesByRoutesAndPaging
{
    public record GetAllBusSchedulesByRoutesAndPagingQueryResponse
    {
        public int Id { get; init; }
        public int TripType { get; init; }
        public int SeatCount { get; init; }
        public CompanyDtoForBusSchedule Company { get; init; }
        public IList<BusRouteDtoForBusSchedule>? BusRoutes { get; init; }
    }
}
