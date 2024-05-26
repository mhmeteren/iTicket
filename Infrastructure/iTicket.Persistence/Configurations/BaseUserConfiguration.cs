using iTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTicket.Persistence.Configurations
{
    public class BaseUserConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.Property(u => u.UserName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.PhoneNumber).HasMaxLength(11).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
            builder.Property(u => u.Gender).HasMaxLength(10).IsRequired(false);
            builder.Property(u => u.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
