using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;

namespace AssetManager.WPF.Service.IService
{
    public interface IPlatformAssetService
    {
        Task<ApiResponse<List<PlatformAssetDto>>> GetAllAsync(long id);

        Task<ApiResponse<PlatformAssetDto>> AddAsync(PlatformAssetParameter query);
    }
}
