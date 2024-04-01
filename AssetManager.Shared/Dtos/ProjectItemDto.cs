namespace AssetManager.Shared.Dtos
{
    public class ProjectItemDto : BaseDto
    {
		private string name;
        private string? hash;
		private string? description;
        private List<ResourcePackageDto> resourcePackages;

        /// <summary>
        /// 项目名
        /// </summary>
        public string Name
		{
			get { return name; }
			set { name = value; OnPropertyChanged(); }
		}

		/// <summary>
		/// 描述
		/// </summary>
        public string? Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 项目的Hash
        /// </summary>
        public string? Hash
		{
			get { return hash; }
			set { hash = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 资源包列表
        /// </summary>
		public List<ResourcePackageDto> ResourcePackages
        {
            get { return resourcePackages; }
            set
            {
                resourcePackages = value;
                OnPropertyChanged();
            }
        }
	}
}
