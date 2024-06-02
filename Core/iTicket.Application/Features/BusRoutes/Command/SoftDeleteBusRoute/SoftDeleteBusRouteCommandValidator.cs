using FluentValidation;

namespace iTicket.Application.Features.BusRoutes.Command.SoftDeleteBusRoute
{
    public class SoftDeleteBusRouteCommandValidator : AbstractValidator<SoftDeleteBusRouteCommandRequest>
    {
        public SoftDeleteBusRouteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
