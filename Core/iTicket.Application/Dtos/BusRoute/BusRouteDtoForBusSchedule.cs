namespace iTicket.Application.Dtos.BusRoute
{
    public record BusRouteDtoForBusSchedule
    {
        public int Id { get; init; }
        public int BusStationId { get; init; }
        public int Price { get; init; }
        public DateTime Date { get; init; }
    }
}
