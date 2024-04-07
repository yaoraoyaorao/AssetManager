using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;

namespace AssetManager.WPF.Service.IService
{
    public interface IPlatformService
    {
        Task<ApiResponse<PlatformDto>> AddAsync(PlatformDto entity);

        Task<ApiResponse<PlatformDto>> UpdateAsync(PlatformDto entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<PlatformDto>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<PagedList<PlatformDto>>> GetAllAsync(QueryParameter query);
    }
}
