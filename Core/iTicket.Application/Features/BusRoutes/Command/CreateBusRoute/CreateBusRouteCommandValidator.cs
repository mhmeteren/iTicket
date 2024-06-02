using FluentValidation;


namespace iTicket.Application.Features.BusRoutes.Command.CreateBusRoute
{
    public class CreateBusRouteCommandValidator : AbstractValidator<CreateBusRouteCommandRequest>
    {
        public CreateBusRouteCommandValidator()
        {
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.BusStationId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Date.Day).NotEmpty().NotNull().GreaterThan(DateTime.Now.Day);
        }
    }
}
