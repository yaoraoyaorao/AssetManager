namespace AssetManager.Shared.Dtos
{
    public class ProjectDto : BaseDto
    {
		private string name;
        private string? guid;
		private string? description;

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
        public string? Guid
        {
			get { return guid; }
			set { guid = value; OnPropertyChanged(); }
        }

	}
}
