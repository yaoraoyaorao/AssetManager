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
        public async Task<ApiResponse> Add([FromBody] ProjectItemFromBody model)
        {
            return await projectItemService.AddAsync(model);
        }

        [HttpPost]
        public async Task<ApiResponse> Update([FromQuery] ProjectItemFromBody parameter)
        {
            return await projectItemService.UpdateAsync(parameter);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter)
        {
            return await projectItemService.GetAllAsync(parameter);
        }

        [HttpGet]
        public async Task<ApiResponse> GetSingle(long Id)
        {
            return await projectItemService.GetSingleAsync(Id);
        }            
        
        [HttpDelete]
        public async Task<ApiResponse> Delete(long Id)
        {
            return await projectItemService.DeleteAsync(Id);
        }
    }
}
