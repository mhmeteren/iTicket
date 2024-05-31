using iTicket.Domain.Common;

namespace iTicket.Domain.Entities
{
    public class Passenger : EntityBase
    {
        public string FullName { get; set; }
        public int Priority { get; set; }

        public string? Gender { get; set; }
        public bool IsNotTurkishCitizen { get; set; }
        public string? IdentificationNo { get; set; }
        public string? PassportNo { get; set; }
        public string? Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<BusSeat> BusSeats { get; set; }

    }
}
