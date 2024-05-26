using iTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTicket.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LogoUrl).IsRequired();
        }
    }
}
