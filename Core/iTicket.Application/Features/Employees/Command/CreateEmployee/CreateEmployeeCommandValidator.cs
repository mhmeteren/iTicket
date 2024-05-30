using FluentValidation;

namespace iTicket.Application.Features.Employees.Command.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommandRequest>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(5);


            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .MinimumLength(8);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(15);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(15);
        }
    }

}
