namespace AssetManager.Shared.Dtos
{
    public class PlatformDto : BaseDto
    {
		private string name;
		private string icon;
		private string? remark;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
                OnPropertyChanged();
            }
        }
    }
}
