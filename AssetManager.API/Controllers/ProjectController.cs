using AssetManager.API.Service.IService;
using AssetManager.Shared;
using AssetManager.Shared.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectItemService projectItemService;

        public ProjectController(IProjectItemService projectItemService)
        {
            this.projectItemService = projectItemService;
        }

        [HttpPost]
        public async Task<ApiResponse> AddAsync([FromBody] ProjectItemFromBody model)
        {
            return await projectItemService.AddAsync(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllAsync([FromQuery] QueryParameter parameter)
        {
            return await projectItemService.GetAllAsync(parameter);
        }
    }
}
