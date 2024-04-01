using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;

namespace AssetManager.API.Context.Repository
{
    public class PlatformRepository: Repository<Platform>, IRepository<Platform>
    {
        public PlatformRepository(AssetManagerContext dbContext) : base(dbContext)
        {

        }
    }
}
