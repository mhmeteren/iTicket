using FluentValidation;

namespace iTicket.Application.Features.Employees.Command.SoftDeleteEmployee
{
    public class SoftDeleteEmployeeCommandValidator : AbstractValidator<SoftDeleteEmployeeCommandRequest>
    {
        public SoftDeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
