using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class PaymentTransaction: IEntityBase
    {
        public int Id { get; set; }
        public string PaymentId { get; set; }
        public string PaymentTransactionId { get; set; }
        public int Status { get; set; } 
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public decimal ConvertedPayoutPaidPrice { get; set; }
        public decimal MerchantPayoutAmount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? TransactionLastUpdateDate { get; set; }


        public int BusSeatId { get; set; }
        public BusSeat BusSeat { get; set; }
    }
}
