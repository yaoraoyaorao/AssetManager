using AssetManager.Shared;
using AssetManager.Shared.Parameters;

namespace AssetManager.API.Service.IService
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<ApiResponse> GetAllAsync(QueryParameter parameter);

        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> GetSingleAsync(long id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResponse> AddAsync(T model);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResponse> UpdateAsync(T model);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteAsync(long id);
    }
}
