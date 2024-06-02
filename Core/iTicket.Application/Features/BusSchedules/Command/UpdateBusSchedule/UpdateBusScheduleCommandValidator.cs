using FluentValidation;
using iTicket.Application.Common.Enums;

namespace iTicket.Application.Features.BusSchedules.Command.UpdateBusSchedule
{
    public class UpdateBusScheduleCommandValidator : AbstractValidator<UpdateBusScheduleCommandRequest>
    {
        public UpdateBusScheduleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);

            RuleFor(x => x.TripType).NotNull()
             .Must(i => Enum.IsDefined(typeof(TripType), i))
             .WithMessage("TripType must be a valid");

            RuleFor(x => x.SeatCount).NotEmpty().NotNull().GreaterThan(0).LessThan(50);

            RuleFor(x => x.BusRouts).NotNull()
                .Must(x => x.Count < 20)
                .WithMessage("Bus Routes list length must be between 0 and 20");
        }
    }
}
