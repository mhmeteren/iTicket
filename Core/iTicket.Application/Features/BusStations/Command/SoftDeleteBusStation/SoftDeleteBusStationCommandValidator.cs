using FluentValidation;

namespace iTicket.Application.Features.BusStations.Command.SoftDeleteBusStation
{
    public class SoftDeleteBusStationCommandValidator : AbstractValidator<SoftDeleteBusStationCommandRequest>
    {
        public SoftDeleteBusStationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
