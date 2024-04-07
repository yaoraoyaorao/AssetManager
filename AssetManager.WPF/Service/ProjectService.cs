using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AssetManager.WPF.Service.Base;
using AssetManager.WPF.Service.IService;
using RestSharp;

namespace AssetManager.WPF.Service
{
    public class ProjectService : IProjectService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;
        public ProjectService(HttpRestClient client)
        {
            this.client = client;
            this.serviceName = "Project";
        }

        public async Task<ApiResponse<ProjectDto>> AddAsync(ProjectItemFromBody model)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Post;

            request.Route = $"api/{serviceName}/Add";

            request.Parameter = model;

            return await client.ExecuteAsync<ProjectDto>(request);
        }

        public async Task<ApiResponse> DeleteAsync(long id)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Delete;

            request.Route = $"api/{serviceName}/Delete?id={id}";

            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<ProjectDto>>> GetAllAsync(Shared.Parameters.QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Get;

            request.Route = $"api/{serviceName}/GetAll?PageIndex={parameter.PageIndex}&PageSize={parameter.PageSize}&Search={parameter.Search}";

            return await client.ExecuteAsync<PagedList<ProjectDto>>(request);
        }

        public async Task<ApiResponse<ProjectDto>> GetSingleAsync(long id)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Get;

            request.Route = $"api/{serviceName}/Get?id={id}";

            return await client.ExecuteAsync<ProjectDto>(request);
        }

        public async Task<ApiResponse<ProjectDto>> UpdateAsync(ProjectItemFromBody model)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Put;

            request.Route = $"api/{serviceName}/Update";
            request.Parameter = model;

            return await client.ExecuteAsync<ProjectDto>(request);
        }
    }
}
