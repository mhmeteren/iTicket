using FluentValidation;

namespace iTicket.Application.Features.BusStations.Command.CreateBusStation
{
    public class CreateBusStationCommandValidator : AbstractValidator<CreateBusStationCommandRequest>
    {
        public CreateBusStationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(64).MinimumLength(3);
            RuleFor(x => x.City).NotEmpty().NotNull().MaximumLength(64).MinimumLength(3);
        }
    }
}
