namespace iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPagingForAdmin
{
    public record GetAllBusStationsByPagingForAdminQueryResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string City { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}
