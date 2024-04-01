using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;
using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Parameters;

namespace AssetManager.API.Service
{
    public class ProjectItemService : IProjectItemService
    {
        private readonly IUnitOfWork work;

        public ProjectItemService(IUnitOfWork unitOfWork)
        {
            this.work = unitOfWork;
        }

        public async Task<ApiResponse> AddAsync(ProjectItemFromBody model)
        {
            try
            {
                var repository = work.GetRepository<Project>();
                var projectItem = new Project()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Guid = Guid.NewGuid().ToString(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                var project = await repository.InsertAsync(projectItem);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "添加成功",
                        Data = projectItem
                    };
                }

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "添加失败",
                };
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Code = 400,
                    Message = "添加失败:" + e.Message,
                };
            }
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<Project>();

                var projects = await repository.GetPagedListAsync
                (
                    predicate: x => 
                    string.IsNullOrWhiteSpace(parameter.Search) ? true : 
                    x.Name.Contains(parameter.Search) || 
                    x.Id.ToString().Contains(parameter.Search) ||
                    x.Guid.Contains(parameter.Search),
                    
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: x => x.OrderByDescending(y => y.CreateTime)
                );

                return new ApiResponse()
                {
                    Code = 200,
                    Message = "获取成功",
                    Data = projects
                };
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Code = 400,
                    Message = "获取失败:" + e.Message,
                };
            }
        }

        public Task<ApiResponse> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateAsync(Project model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateAsync(ProjectItemFromBody model)
        {
            throw new NotImplementedException();
        }
    }
}
