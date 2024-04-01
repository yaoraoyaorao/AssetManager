using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;

namespace AssetManager.API.Context.Repository
{
    public class ProjectItemRepository : Repository<Project>, IRepository<Project>
    {
        public ProjectItemRepository(AssetManagerContext dbContext) : base(dbContext)
        {
        }
    }
}
