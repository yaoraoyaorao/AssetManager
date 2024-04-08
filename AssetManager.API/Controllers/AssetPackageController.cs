using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AssetPackageController : ControllerBase
    {
        private readonly IAssetPackageService service;

        public AssetPackageController(IAssetPackageService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery]QueryParameter query) => await service.GetAllAsync(query);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody]AssetPackageParameter query) =>await service.AddAsync(query);
    }
}
