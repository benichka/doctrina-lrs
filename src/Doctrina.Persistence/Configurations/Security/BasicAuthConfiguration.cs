using Doctrina.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctrina.Persistence.Configurations
{
    public class BasicAuthConfiguration : IEntityTypeConfiguration<BasicAuth>
    {
        public void Configure(EntityTypeBuilder<BasicAuth> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.AllowedHosts);

            builder.Property(e => e.Username)
                .HasMaxLength(40);

            builder.Property(e => e.Password)
                .HasMaxLength(40);
        }
    }
}
