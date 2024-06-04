using iTicket.Application.Dtos.Payments;
using iTicket.Application.Interfaces.Payments;
using iTicket.Domain.Entities;
using Iyzipay.Model;
using Iyzipay.Request;
using MediatR;
using Microsoft.Extensions.Options;

namespace iTicket.Infrastructure.Payments
{
    public class IyzipayPaymentService(IOptions<Iyzipay.Options> paymentSettings) : IPaymentService
    {
        private readonly Iyzipay.Options options = paymentSettings.Value;


        public Payment CreatePaymentRequest(PaymentRequest paymentRequest)
        {
            int totalPrice = CalculateTotalPrice(paymentRequest.BusSeats);

            CreatePaymentRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = totalPrice.ToString(),
                PaidPrice = totalPrice.ToString(),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                PaymentCard = MapPaymentCard(paymentRequest.PaymentCard),
                Buyer = MapBuyer(paymentRequest.BuyerUser),
                BasketItems = MapBasketItems(paymentRequest.BusSeats),
                BillingAddress = GetbillingAddress()
            };
            return Payment.Create(request, options);
        }

        private int CalculateTotalPrice(IList<BusSeat> busSeats) => busSeats.Sum(x => x.Price);
        private PaymentCard MapPaymentCard(PaymentCardDto PaymentCard) =>
            new()
            {
                CardHolderName = PaymentCard.CardHolderName,
                CardNumber = PaymentCard.CardNumber,
                ExpireMonth = PaymentCard.ExpireMonth,
                ExpireYear = PaymentCard.ExpireYear,
                Cvc = PaymentCard.Cvc,
                RegisterCard = 0
            };
        private Buyer MapBuyer(Passenger user)
        {
            string[] fullnameArr = user.FullName.Split(' ');

            return new()
            {
                Id = user.UserId.ToString(),
                Name = fullnameArr[0],
                Surname = fullnameArr[1],
                IdentityNumber = user.IdentificationNo ?? user.PassportNo,
                RegistrationAddress = "Ankara merkez Camii'nin hemen yanındaki marketin karşısındaki evin 3.katı, kapı no: 13",
                Email = "email@email.com",
                City = "Ankara",
                Country = "Turkey",
                Ip = "127.0.0.1",
            };
        }

        private List<BasketItem> MapBasketItems(IList<BusSeat> busSeats)
        {
            List<BasketItem> basketItems = [];

            foreach (BusSeat busSeat in busSeats)
            {
                basketItems.Add(new()
                {
                    Id = busSeat.PassengerId.ToString(),
                    Name = GenerateBasketItemName(busSeat),
                    Category1 = "Ticket",
                    ItemType = BasketItemType.VIRTUAL.ToString(),
                    Price = busSeat.Price.ToString()
                });
            }


            return basketItems;
        }

        private string GenerateBasketItemName(BusSeat busSeat) =>
            $"BusTicket:{busSeat.BusScheduleId}:{busSeat.StartBusRouteId}:{busSeat.EndBusRouteId}:{busSeat.PassengerId}:{busSeat.SeatNumber}";

        private Address GetbillingAddress() =>
            new()
            {
                ContactName = "Justin Timberlake",
                City = "Ankara",
                Country = "Turkey",
                Description = "Ankara merkez Camii'nin hemen yanındaki marketin karşısındaki evin 3.katı, kapı no: 13",
                ZipCode = "010101"
            };

    }
}
