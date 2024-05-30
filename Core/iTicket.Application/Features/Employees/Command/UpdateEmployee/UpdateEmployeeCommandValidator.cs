using FluentValidation;

namespace iTicket.Application.Features.Employees.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommandRequest>
    {
        public UpdateEmployeeCommandValidator()
        {

            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().GreaterThan(0);

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(5);


            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .MinimumLength(8);
        }
    }
}
