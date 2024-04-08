using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;

namespace AssetManager.WPF.Service.IService
{
    public interface IAssetPackageService
    {
        Task<ApiResponse<AssetPackageDto>> AddAsync(AssetPackageParameter assetPackageParameter);

        Task<ApiResponse<PagedList<AssetPackageDto>>> GetAllAsync(QueryParameter query);
    }
}
