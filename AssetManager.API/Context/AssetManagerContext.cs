using AssetManager.API.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.API.Context
{
    public class AssetManagerContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<PlatformAsset> PlatformAssets { get; set; }
        public DbSet<AssetPackage> ResourcePackages { get; set; }
        public DbSet<Project> ProjectItems { get; set; }

        public AssetManagerContext(DbContextOptions<AssetManagerContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //从当前程序集中加载所有的IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
