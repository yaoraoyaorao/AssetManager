using AssetManager.API.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.API.Context.Configuration
{
    public class AssetPackageConfig : IEntityTypeConfiguration<AssetPackage>
    {
        public void Configure(EntityTypeBuilder<AssetPackage> builder)
        {
            builder.Property(a => a.Max).IsRequired().HasDefaultValue(1);
            builder.Property(a => a.Min).IsRequired().HasDefaultValue(0);
            builder.Property(a => a.Patch).IsRequired().HasDefaultValue(0);
            builder.Property(a => a.AuditStatus).IsRequired().HasDefaultValue(0);

            builder.HasOne(a => a.TargetProject).WithMany(p => p.AssetPackages);
        }
    }
}
