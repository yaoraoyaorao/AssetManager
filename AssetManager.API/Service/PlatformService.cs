using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;
using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AutoMapper;

namespace AssetManager.API.Service
{
    public class PlatformService : IPlatformService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public PlatformService(IUnitOfWork work,IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(PlatformDto model)
        {
            try
            {
                var repository = work.GetRepository<Platform>();

                var platform = mapper.Map<Platform>(model);
                platform.UpdateTime = DateTime.Now;
                platform.CreateTime = DateTime.Now;
           
                await repository.InsertAsync(platform);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "添加成功",
                        Data = model
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
                    Message = "添加失败：" + e.Message,
                };
            }
        }

        public async Task<ApiResponse> DeleteAsync(long id)
        {
            try
            {
                var repository = work.GetRepository<Platform>();

                var platform = await repository.GetFirstOrDefaultAsync(predicate:x=>x.Id == id);
            
                repository.Delete(platform);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "删除成功",
                    };
                }

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "删除失败："
                };
            }
            catch (Exception e)
            {

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "删除失败：" + e.Message,
                };
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<Platform>();

                var platforms = await repository.GetPagedListAsync(
                    predicate: x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize);

                return new ApiResponse()
                {
                    Code = 200,
                    Message = "获取成功",
                    Data = platforms
                };
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Code = 400,
                    Message = "获取失败：" + e.Message,
                };
            }
        }

        public async Task<ApiResponse> GetSingleAsync(long id)
        {
            try
            {
                var repository = work.GetRepository<Platform>();

                var platform = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);

                if (platform == null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"获取失败 id:{id}不存在"
                    };
                }

                var platformDto = mapper.Map<PlatformDto>(platform);
                return new ApiResponse()
                {
                    Code = 200,
                    Message = "获取成功",
                    Data = platformDto
                };
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Code = 400,
                    Message = "获取失败：" + e.Message,
                };
            }
        }

        public async Task<ApiResponse> UpdateAsync(PlatformDto model)
        {
            try
            {
                var repository = work.GetRepository<Platform>();
                
                var platform = mapper.Map<Platform>(model);

                platform.UpdateTime = DateTime.Now;

                repository.Update(platform);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "更新成功"
                    };
                }

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "更新失败：",
                };
            }
            catch (Exception e)
            {

                return new ApiResponse()
                {
                    Code = 400,
                    Message = "更新失败：" + e.Message,
                };
            }
        }
    }
}
