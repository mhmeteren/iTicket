using MediatR;

namespace iTicket.Application.Features.Employees.Command.SoftDeleteEmployee
{
    public record SoftDeleteEmployeeCommandRequest :IRequest<Unit>
    {
        public string Id { get; init; }
    }
}
