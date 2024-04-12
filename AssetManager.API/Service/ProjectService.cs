using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;
using AssetManager.API.Extensions;
using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AutoMapper;

namespace AssetManager.API.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;
        private readonly IAssetUtility utility;

        public ProjectService(IUnitOfWork unitOfWork,IMapper mapper,IAssetUtility utility)
        {
            this.work = unitOfWork;
            this.mapper = mapper;
            this.utility = utility;
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> AddAsync(ProjectItemFromBody model)
        {
            try
            {
                var repository = work.GetRepository<Project>();
                
                var p = await repository.GetFirstOrDefaultAsync(predicate: x => x.Name == model.Name);

                if (p != null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"项目名已存在:{model.Name}",
                    };
                }

                var projectItem = new Project()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Guid = Guid.NewGuid().ToString("N"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                var project = await repository.InsertAsync(projectItem);

                if (await work.SaveChangesAsync() > 0)
                {

                    var data = mapper.Map<ProjectDto>(projectItem);

                    utility.CreateOrUpdateFolder(data.Guid);

                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "添加成功",
                        Data = data
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

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> DeleteAsync(long id)
        {
            try
            {
                var repository = work.GetRepository<Project>();

                var project = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);

                if (project == null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"删除失败id:{project.Id}不存在",
                    };
                }

                repository.Delete(project);

                if (await work.SaveChangesAsync() > 0)
                {
                    utility.DeleteFolder(project.Name);
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "删除成功",
                    };
                }

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "删除失败",
                };
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Code = 400,
                    Message = "删除失败:" + e.Message,
                };
            }
        }

        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取单个项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetSingleAsync(long id)
        {
            try
            {
                var repository = work.GetRepository<Project>();

                var project = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);

                if (project == null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"获取失败 id:{id}不存在"
                    };
                }


                var projectDto = mapper.Map<ProjectDto>(project);
                return new ApiResponse()
                {
                    Code = 200,
                    Message = $"获取成功",
                    Data = projectDto
                };

            }
            catch (Exception e)
            {

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "获取失败" + e.Message,
                };
            }
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse> UpdateAsync(ProjectItemFromBody model)
        {
            try
            {
                var repository = work.GetRepository<Project>();
                var project = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == model.Id);

                if (project == null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"更新失败 id:{model.Id}不存在"
                    };
                }

                string oldProjectName = project.Name;
                project.Name = model.Name;
                project.Description = model.Description;

                repository.Update(project);

                if (await work.SaveChangesAsync() > 0)
                {
                    utility.CreateOrUpdateFolder(oldProjectName, model.Name);

                    var projectDto = mapper.Map<ProjectDto>(project);
                    
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "更新成功",
                        Data = projectDto
                    };
                }

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "更新失败:",
                };
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Code = 400,
                    Message = "更新失败:" + e.Message,
                };
            }
        }
    }
}
