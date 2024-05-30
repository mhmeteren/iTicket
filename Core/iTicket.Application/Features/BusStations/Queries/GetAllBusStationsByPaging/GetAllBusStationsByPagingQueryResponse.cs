namespace iTicket.Application.Features.BusStations.Queries.GetAllBusStationsByPaging
{
    public record GetAllBusStationsByPagingQueryResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string City { get; init; }
    }
}
