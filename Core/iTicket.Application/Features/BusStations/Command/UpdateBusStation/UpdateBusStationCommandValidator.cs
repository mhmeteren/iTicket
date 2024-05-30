using FluentValidation;

namespace iTicket.Application.Features.BusStations.Command.UpdateBusStation
{
    public class UpdateBusStationCommandValidator : AbstractValidator<UpdateBusStationCommandRequest>
    {
        public UpdateBusStationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(64).MinimumLength(3);
            RuleFor(x => x.City).NotEmpty().NotNull().MaximumLength(64).MinimumLength(3);
        }
    }
}
