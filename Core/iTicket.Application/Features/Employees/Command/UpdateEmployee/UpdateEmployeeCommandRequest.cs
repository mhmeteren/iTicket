using MediatR;

namespace iTicket.Application.Features.Employees.Command.UpdateEmployee
{
    public record UpdateEmployeeCommandRequest :IRequest<Unit>
    {
        public string Id { get; init; }
        public int CompanyId { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
    }
}
