using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlatformController
    {
        private readonly IPlatformService service;

        public PlatformController(IPlatformService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] PlatformDto p) =>await service.AddAsync(p);

        [HttpDelete]
        public async Task<ApiResponse> Delete(long id) => await service.DeleteAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter query) => await service.GetAllAsync(query);

        [HttpGet]
        public async Task<ApiResponse> GetSingle(long id) => await service.GetSingleAsync(id);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] PlatformDto p) => await service.UpdateAsync(p);
    }
}
