using Iyzipay.Model;

namespace iTicket.Application.Interfaces.Payments
{
    public interface IPaymentService
    {
        Payment CreatePaymentRequest(PaymentRequest paymentRequest);
    }
}
