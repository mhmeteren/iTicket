using FluentValidation;
using iTicket.Application.Common.Enums;

namespace iTicket.Application.Features.BusSchedules.Command.CreateBusSchedule
{
    public class CreateBusScheduleCommandValidator: AbstractValidator<CreateBusScheduleCommandRequest>
    {
        public CreateBusScheduleCommandValidator()
        {
            RuleFor(x => x.TripType).NotNull()
                .Must(i => Enum.IsDefined(typeof(TripType), i))
                .WithMessage("TripType must be a valid");

            RuleFor(x => x.SeatCount).NotEmpty().NotNull().GreaterThan(0).LessThan(50);

            RuleFor(x => x.BusRouts).NotEmpty().NotNull()
                .Must(x => x.Count is < 20 and > 0)
                .WithMessage("Bus Routes list length must be between 1 and 20");

        }
    }

}
