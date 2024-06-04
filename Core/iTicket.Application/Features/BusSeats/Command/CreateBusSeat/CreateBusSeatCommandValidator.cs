using FluentValidation;

namespace iTicket.Application.Features.BusSeats.Command.CreateBusSeat
{
    public class CreateBusSeatCommandValidator : AbstractValidator<CreateBusSeatCommandRequest>
    {
        public CreateBusSeatCommandValidator()
        {
            RuleFor(x => x.StartBusRouteId).NotEmpty().NotNull();
            RuleFor(x => x.EndBusRouteId).NotEmpty().NotNull();
            RuleFor(x => x.BusScheduleId).NotEmpty().NotNull();
            RuleFor(x => x.Passangers).NotEmpty().NotNull().Must(x => x.Count is >0 and <10);


            RuleFor(x => x.PaymentCard).NotEmpty().NotNull();
            RuleFor(x => x.PaymentCard.CardHolderName).NotEmpty().NotNull();
            RuleFor(x => x.PaymentCard.CardNumber).NotEmpty().NotNull();
            RuleFor(x => x.PaymentCard.ExpireYear).NotEmpty().NotNull();
            RuleFor(x => x.PaymentCard.ExpireMonth).NotEmpty().NotNull();
            RuleFor(x => x.PaymentCard.Cvc).NotEmpty().NotNull();

        }
    }
}
