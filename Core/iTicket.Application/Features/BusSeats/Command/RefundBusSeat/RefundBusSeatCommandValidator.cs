using FluentValidation;

namespace iTicket.Application.Features.BusSeats.Command.RefundBusSeat
{
    public class RefundBusSeatCommandValidator : AbstractValidator<RefundBusSeatCommandRequest>
    {
        public RefundBusSeatCommandValidator()
        {
            RuleFor(x => x.BusSeatId).NotEmpty().NotNull();
        }
    }
}
