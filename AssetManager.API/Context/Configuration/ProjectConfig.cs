using AssetManager.API.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.API.Context.Configuration
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Guid).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
        }
    }
}
