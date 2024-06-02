using FluentValidation;

namespace iTicket.Application.Features.BusRoutes.Command.UpdateBusRoute
{
    public class UpdateBusRouteCommandValidator : AbstractValidator<UpdateBusRouteCommandRequest>
    {
        public UpdateBusRouteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.BusStationId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Date.Day).NotEmpty().NotNull().GreaterThan(DateTime.Now.Day);
        }
    }
}
