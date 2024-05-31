using FluentValidation;

namespace iTicket.Application.Features.Passengers.Command.SoftDeletePassenger
{
    public class SoftDeletePassengerCommandValidator : AbstractValidator<SoftDeletePassengerCommandRequest>
    {
        public SoftDeletePassengerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
