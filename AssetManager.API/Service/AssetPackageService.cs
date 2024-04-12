using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;
using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.API.Service
{
    public class AssetPackageService : IAssetPackageService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public AssetPackageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.work = unitOfWork;
            this.mapper = mapper;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="assetPackageParameter"></param>
        /// <returns></returns>
        public async Task<ApiResponse> AddAsync(AssetPackageParameter assetPackageParameter)
        {
            try
            {
                if (assetPackageParameter.Max < 1 || assetPackageParameter.Min < 0 || assetPackageParameter.Patch < 0)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = "Max需要从1开始，Min和Patch要从0开始"
                    };
                }

                var repository = work.GetRepository<Project>();

                var project = await repository.GetFirstOrDefaultAsync(
                   predicate: x => x.Id == assetPackageParameter.Id,
                   include:source=>source.Include(b=>b.AssetPackages)
                   );

                if (project == null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = "添加失败:项目不存在 id:" + assetPackageParameter.Id,
                    };
                }

                var packages = project.AssetPackages.Where(a =>
                {
                    string version = a.Max + "." + a.Min + "." + a.Patch;
                    string target = assetPackageParameter.Max + "." + assetPackageParameter.Min + "." + assetPackageParameter.Patch;

                    return version == target;
                });

                if (packages.Count() > 0)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = "添加失败:已存在相同版本的资源",
                    };
                }

                var assetPackage = new AssetPackage()
                {
                    Max = assetPackageParameter.Max,
                    Min = assetPackageParameter.Min,
                    Patch = assetPackageParameter.Patch,
                    TargetProject = project,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                project.AssetPackages.Add(assetPackage);
                project.UpdateTime = DateTime.Now;

                repository.Update(project);

                if (await work.SaveChangesAsync() > 0)
                {
                    var asset = mapper.Map<AssetPackageDto>(assetPackage);
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = "添加成功",
                        Data = asset
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
        /// 获取平台资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            try
            {
                var repository = work.GetRepository<AssetPackage>();
                var assets = await repository.GetPagedListAsync(
                    predicate: x =>
                     x.TargetProject.Id == query.Id &&
                    (string.IsNullOrWhiteSpace(query.Search) ||
                    x.Max + "." + x.Min + "." + x.Patch == query.Search),

                    pageIndex: query.PageIndex,
                    pageSize: query.PageSize
                    );

                if (assets == null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"获取失败id:{query.Id}不存在",
                    };
                }

                //var assetDtos = mapper.Map<List<AssetPackageDto>>(assets.Items);
                return new ApiResponse()
                {
                    Code = 200,
                    Message = "获取成功",
                    Data = assets
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

       
    }
}
