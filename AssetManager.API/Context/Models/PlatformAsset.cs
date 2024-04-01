namespace AssetManager.API.Context.Models
{
    /// <summary>
    /// 平台资源对象
    /// </summary>
    public class PlatformAsset : BaseEntity
    {
        /// <summary>
        /// 目标平台
        /// </summary>
        public Platform TargetPlatform { get; set; }

        /// <summary>
        /// 资源路径
        /// </summary>
        public string AssetPath { get; set; }

        /// <summary>
        /// 目标资源包
        /// </summary>
        public AssetPackage TargetAssetPackage { get; set; }
    }
}
