namespace iTicket.Domain.Entities
{
    public class User : BaseUser
    {
        public ICollection<Passenger> Passengers { get; set; }
    }
}
