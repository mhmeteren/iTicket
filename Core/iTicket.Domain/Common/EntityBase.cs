namespace iTicket.Domain.Common
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
