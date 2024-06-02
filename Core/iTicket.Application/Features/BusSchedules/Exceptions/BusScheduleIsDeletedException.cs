using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSchedules.Exceptions
{
    public class BusScheduleIsDeletedException() : BaseNotFoundException("Bus Schedule not found.")
    {
    }
}
