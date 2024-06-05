using System.Globalization;

namespace iTicket.Application.Interfaces.Payments
{
    public class RefundRequest(decimal price, string paymentTransactionId)
    {
        public string Price { get; set; } = price.ToString("0.00", CultureInfo.InvariantCulture);
        public string PaymentTransactionId { get; set; } = paymentTransactionId;
    }
}
