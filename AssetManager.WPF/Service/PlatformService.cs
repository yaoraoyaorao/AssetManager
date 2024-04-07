using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.WPF.Service.Base;
using AssetManager.WPF.Service.IService;
using RestSharp;

namespace AssetManager.WPF.Service
{
    public class PlatformService: IPlatformService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;

        public PlatformService(HttpRestClient client)
        {
            this.client = client;
            this.serviceName = "Platform";
        }

        public async Task<ApiResponse<PlatformDto>> AddAsync(PlatformDto entity)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Post;

            request.Route = $"api/{serviceName}/Add";
            request.Parameter = entity;

            return await client.ExecuteAsync<PlatformDto>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Delete;

            request.Route = $"api/{serviceName}/Delete?id={id}";

            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<PlatformDto>>> GetAllAsync(Shared.Parameters.QueryParameter query)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Get;

            request.Route = $"api/{serviceName}/GetAll?PageIndex={query.PageIndex}&PageSize={query.PageSize}&Search={query.Search}";

            return await client.ExecuteAsync<PagedList<PlatformDto>>(request);
        }

        public async Task<ApiResponse<PlatformDto>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Get;

            request.Route = $"api/{serviceName}/Get?id={id}";

            return await client.ExecuteAsync<PlatformDto>(request);
        }

        public async Task<ApiResponse<PlatformDto>> UpdateAsync(PlatformDto entity)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Put;

            request.Route = $"api/{serviceName}/Update";
            request.Parameter = entity;

            return await client.ExecuteAsync<PlatformDto>(request);
        }
    }
}
