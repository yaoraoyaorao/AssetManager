﻿using Arch.EntityFrameworkCore.UnitOfWork;
using AssetManager.API.Context.Models;
using AssetManager.API.Extensions;
using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.API.Service
{
    /// <summary>
    /// 平台资源服务
    /// </summary>
    public class PlatformAssetService : IPlatformAssetService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;
        private readonly IAssetUtility utility;

        public PlatformAssetService(IUnitOfWork unitOfWork, IMapper mapper,IAssetUtility utility)
        {
            this.work = unitOfWork;
            this.mapper = mapper;
            this.utility = utility;
        }

        /// <summary>
        /// 添加平台资源
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ApiResponse> AddAsync(PlatformAssetParameter query)
        {
            try
            {
                var repositoryAsset = work.GetRepository<AssetPackage>();
                var repositoryPlatform = work.GetRepository<Platform>();
                var repositoryPlatformAsset = work.GetRepository<PlatformAsset>();

                //获取资源包
                var assetPackage = await repositoryAsset.GetFirstOrDefaultAsync(
                    predicate: x => x.Id == query.AssetPackageId,
                    include:source=>source.Include(t=>t.TargetProject).Include(t=>t.PlatformAssets).ThenInclude(pa=>pa.TargetPlatform));

                if (assetPackage == null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"添加失败:资源包Id不存在{query.AssetPackageId}"
                    };
                }

                //判断不能又相同平台
                var test =  assetPackage.PlatformAssets.FirstOrDefault(x => x.TargetPlatform.Id == query.PlatformId);
                if (test != null)
                {
                    return new ApiResponse()
                    {
                        Code = 400,
                        Message = $"添加失败:平台已存在 {query.PlatformId}"
                    };
                }

                //获取平台
                var platform = await repositoryPlatform.GetFirstOrDefaultAsync(predicate: x => query.PlatformId == x.Id);

                //构建平添数据
                var platformAsset = new PlatformAsset()
                {
                    TargetPlatform = platform,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    AssetPath = Path.Combine(
                    utility.dataPath,
                    assetPackage.TargetProject.Name,
                    assetPackage.Max.ToString(),
                    assetPackage.Min.ToString(),
                    assetPackage.Patch.ToString(),
                    platform.Name)
                };

                assetPackage.PlatformAssets.Add(platformAsset);
                assetPackage.UpdateTime = DateTime.Now;
                repositoryAsset.Update(assetPackage);

                if (await work.SaveChangesAsync() > 0)
                {
                    utility.CreateFolders(assetPackage.TargetProject.Name,
                         assetPackage.Max.ToString(),
                         assetPackage.Min.ToString(),
                         assetPackage.Patch.ToString(),
                         platform.Name);

                    var dto = mapper.Map<PlatformAssetDto>(platformAsset);
                    return new ApiResponse()
                    {
                        Code = 200,
                        Message = $"添加成功",
                        Data= dto
                    };
                } 

                return new ApiResponse()
                {
                    Code = 400,
                    Message = $"添加失败:",
                };
            }
            catch (Exception e)
            {

                return new ApiResponse()
                {
                    Code = 400,
                    Message = $"添加失败:" + e.Message,
                };
            }
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse> GetAllAsync(long id)
        {
            try
            {
                var repository = work.GetRepository<PlatformAsset>();

                var platformAssets = await repository.GetPagedListAsync(
                                    predicate:x=>x.TargetAssetPackage.Id == id,
                                    include: source => source.Include(c=>c.TargetPlatform));
                var items = mapper.Map<List<PlatformAssetDto>>(platformAssets.Items);
                return new ApiResponse()
                {
                    Code = 200,
                    Message = "获取成功",
                    Data = items
                };
            }
            catch (Exception e)
            {

                return new ApiResponse()
                {
                    Code = 400,
                    Message = $"获取失败:" + e.Message
                };
            }
        }
    }
}
