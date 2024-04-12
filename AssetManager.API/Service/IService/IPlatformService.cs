using AssetManager.Shared;
using AssetManager.Shared.Dtos;

namespace AssetManager.API.Service.IService
{
    public interface IPlatformService:IBaseService<PlatformDto>
    {

        /// <summary>
        /// 获取可以使用的平台
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> GetCanUsePlatformAsync(long id);
    }
}
