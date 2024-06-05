using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class BusSeatShouldBeExistsException() : BaseNotFoundException("Ticket not found")
    {
    }
}
