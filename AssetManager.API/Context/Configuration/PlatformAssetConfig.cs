using AssetManager.API.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.API.Context.Configuration
{
    public class PlatformAssetConfig : IEntityTypeConfiguration<PlatformAsset>
    {
        public void Configure(EntityTypeBuilder<PlatformAsset> builder)
        {
            builder.HasOne(p => p.TargetPlatform).WithMany().IsRequired();

            builder.HasOne(p => p.TargetAssetPackage).WithMany(a => a.PlatformAssets);
            
            builder.Property(p=>p.AssetPath).IsRequired(false);
        }
    }
}
