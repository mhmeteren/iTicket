using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSeats.Exceptions
{
    public class PrimaryPassangerShouldBeExistsExcecption() : BaseBadRequestException("Primary passanger should be exists")
    {
    }
}
