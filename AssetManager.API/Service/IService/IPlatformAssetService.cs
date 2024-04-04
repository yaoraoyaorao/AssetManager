using AssetManager.Shared;
using AssetManager.Shared.Parameters;

namespace AssetManager.API.Service.IService
{

    public interface IPlatformAssetService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ApiResponse> AddAsync(PlatformAssetParameter query);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> GetAllAsync(long id);
    }
}
