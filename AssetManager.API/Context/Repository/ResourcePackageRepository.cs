using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;

namespace AssetManager.API.Context.Repository
{
    public class ResourcePackageRepository: Repository<AssetPackage>, IRepository<AssetPackage>
    {
        public ResourcePackageRepository(AssetManagerContext dbContext) : base(dbContext) { }
    }
}
