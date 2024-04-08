using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AssetManager.WPF.Service.Base;
using AssetManager.WPF.Service.IService;
using RestSharp;

namespace AssetManager.WPF.Service
{
    public class AssetPackageService : IAssetPackageService
    {

        private readonly HttpRestClient client;
        private readonly string serviceName;
        public AssetPackageService(HttpRestClient client)
        {
            this.client = client;
            this.serviceName = "AssetPackage";
        }

        public async Task<ApiResponse<AssetPackageDto>> AddAsync(AssetPackageParameter assetPackageParameter)
        {
            BaseRequest request = new BaseRequest();

            request.Method = Method.Post;

            request.Route = $"api/{serviceName}/Add";
            request.Parameter = assetPackageParameter;

            return await client.ExecuteAsync<AssetPackageDto>(request);
        }

        public async Task<ApiResponse<PagedList<AssetPackageDto>>> GetAllAsync(Shared.Parameters.QueryParameter query)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetAll?Id={query.Id}&PageIndex={query.PageIndex}&PageSize={query.PageSize}&Search={query.Search}";

            return await client.ExecuteAsync<PagedList<AssetPackageDto>>(request);
        }
    }
}
