namespace AssetManager.API.Context.Models
{
    /// <summary>
    /// 资源包
    /// </summary>
    public class AssetPackage : BaseEntity
    {
        /// <summary>
        /// 大版本
        /// </summary>
        public int Max { get; set; }
        
        /// <summary>
        /// 小版本
        /// </summary>
        public int Min { get; set; }
        
        /// <summary>
        /// 补丁版本
        /// </summary>
        public int Patch { get; set; }
        
        /// <summary>
        /// 审核状态
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 平台资源列表
        /// </summary>
        public List<PlatformAsset> PlatformAssets { get; set; } = new List<PlatformAsset>();
        
        /// <summary>
        /// 目标项目
        /// </summary>
        public Project TargetProject { get; set; }
    }
}
