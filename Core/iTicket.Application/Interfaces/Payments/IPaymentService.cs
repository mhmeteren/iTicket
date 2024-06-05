using Iyzipay.Model;

namespace iTicket.Application.Interfaces.Payments
{
    public interface IPaymentService
    {
        Refund RefundPaymentRequest(RefundRequest refundRequest);
        Payment CreatePaymentRequest(PaymentRequest paymentRequest);
    }
}
