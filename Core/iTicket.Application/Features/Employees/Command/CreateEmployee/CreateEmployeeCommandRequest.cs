using MediatR;

namespace iTicket.Application.Features.Employees.Command.CreateEmployee
{
    public record CreateEmployeeCommandRequest : IRequest<Unit>
    {
        public int CompanyId { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
    }
}
