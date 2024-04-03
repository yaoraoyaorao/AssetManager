namespace AssetManager.API.Context.Models
{
    /// <summary>
    /// 项目对象
    /// </summary>
    public class Project : BaseEntity
    {
        /// <summary>
        /// 项目名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 项目Guid
        /// </summary>
        public string? Guid { get; set; }

        /// <summary>
        /// 资源包列表
        /// </summary>
        public List<AssetPackage> AssetPackages { get; set; } = new List<AssetPackage>();
    }
}
