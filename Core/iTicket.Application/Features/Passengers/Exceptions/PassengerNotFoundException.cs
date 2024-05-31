using iTicket.Application.Bases;

namespace iTicket.Application.Features.Passengers.Exceptions
{
    public class PassengerNotFoundException() : BaseNotFoundException("Passenger not found") { }
}
