using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AssetManager.WPF.Service.Base;
using AssetManager.WPF.Service.IService;
using RestSharp;

namespace AssetManager.WPF.Service
{
    public class PlatformAssetService : IPlatformAssetService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;
        public PlatformAssetService(HttpRestClient client)
        {
            this.client = client;
            this.serviceName = "PlatformAsset";
        }

        public async Task<ApiResponse<PlatformAssetDto>> AddAsync(PlatformAssetParameter query)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Post;

            request.Route = $"api/{serviceName}/Add";

            request.Parameter = query;

            return await client.ExecuteAsync<PlatformAssetDto>(request);
        }

        public async Task<ApiResponse<List<PlatformAssetDto>>> GetAllAsync(long id)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Get;

            request.Route = $"api/{serviceName}/GetAll?id={id}";

            return await client.ExecuteAsync<List<PlatformAssetDto>>(request);
        }
    }
}
