using iTicket.Application.Dtos.Payments;
using iTicket.Domain.Entities;

namespace iTicket.Application.Interfaces.Payments
{
    public class PaymentRequest(Passenger primaryPassenger, IList<BusSeat> busSeats, PaymentCardDto paymentCard)
    {
        public Passenger BuyerUser { get; set; } = primaryPassenger;
        public IList<BusSeat> BusSeats { get; set; } = busSeats;
        public PaymentCardDto PaymentCard { get; set; } = paymentCard;
    }
}