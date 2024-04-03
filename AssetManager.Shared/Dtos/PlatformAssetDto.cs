namespace AssetManager.Shared.Dtos
{
    public class PlatformAssetDto : BaseDto
    {
        private string assetPath;
        private PlatformDto platformDto;

        /// 资源路径
        /// </summary>
        public string AssetPath
        {
            get
            {
                return assetPath;
            }
            set
            {
                assetPath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 目标平台
        /// </summary>
        public PlatformDto TargetPlatform
        {
            get
            {
                return platformDto;
            }
            set
            {
                platformDto = value;
                OnPropertyChanged();
            }
        }
    }
}
