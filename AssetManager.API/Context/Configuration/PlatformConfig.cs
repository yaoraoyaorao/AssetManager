using AssetManager.API.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.API.Context.Configuration
{
    public class PlatformConfig : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Icon).IsRequired();
            builder.Property(p=>p.Remark).HasMaxLength(300);

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
