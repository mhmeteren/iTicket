using iTicket.Application.Bases;

namespace iTicket.Application.Features.BusSchedules.Exceptions
{
    public class OwnerEmployeeIsDeletedException() : BaseBadRequestException("Employee account is invalid.")
    {
    }
}
