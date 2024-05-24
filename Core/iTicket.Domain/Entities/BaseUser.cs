using Microsoft.AspNetCore.Identity;

namespace iTicket.Domain.Entities
{
    public class BaseUser : IdentityUser<Guid>
    {   
        public string? Gender { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime CreateDate { get; set; }


        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireTime { get; set; }
    }
}
