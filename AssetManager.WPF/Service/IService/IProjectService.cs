using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;

namespace AssetManager.WPF.Service.IService
{
    public interface IProjectService
    {
        Task<ApiResponse<PagedList<ProjectDto>>> GetAllAsync(QueryParameter parameter);

        Task<ApiResponse<ProjectDto>> GetSingleAsync(long id);

        Task<ApiResponse<ProjectDto>> AddAsync(ProjectItemFromBody model);

        Task<ApiResponse<ProjectDto>> UpdateAsync(ProjectItemFromBody model);

        Task<ApiResponse> DeleteAsync(long id);
    }
}
