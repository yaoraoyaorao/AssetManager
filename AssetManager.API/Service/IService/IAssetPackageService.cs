using AssetManager.Shared;
using AssetManager.Shared.Parameters;

namespace AssetManager.API.Service.IService
{
    public interface IAssetPackageService
    {
        /// <summary>
        /// 获取所有所有资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> GetAllAsync(QueryParameter query);

        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> AddAsync(AssetPackageParameter assetPackageParameter);
    }
}
