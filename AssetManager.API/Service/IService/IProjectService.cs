using AssetManager.Shared;
using AssetManager.Shared.Parameters;

namespace AssetManager.API.Service.IService
{
    public interface IProjectService
    {
        Task<ApiResponse> GetAllAsync(QueryParameter parameter);

        Task<ApiResponse> GetSingleAsync(long id);

        Task<ApiResponse> AddAsync(ProjectItemFromBody model);

        Task<ApiResponse> UpdateAsync(ProjectItemFromBody model);

        Task<ApiResponse> DeleteAsync(long id);
    }
}
