using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlatformAssetController : ControllerBase
    {
        private readonly IPlatformAssetService service;

        public PlatformAssetController(IPlatformAssetService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] PlatformAssetParameter query) => await service.AddAsync(query);

        [HttpGet]
        public async Task<ApiResponse> GetAll(long id) => await service.GetAllAsync(id);

       
    }
}
