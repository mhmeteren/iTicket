using FluentValidation;

namespace iTicket.Application.Features.BusSchedules.Command.SoftDeleteBusSchedule
{
    public class SoftDeleteBusScheduleCommandValidator : AbstractValidator<SoftDeleteBusScheduleCommandRequest>
    {
        public SoftDeleteBusScheduleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
