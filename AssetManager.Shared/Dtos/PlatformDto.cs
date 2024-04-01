namespace AssetManager.Shared.Dtos
{
    public class PlatformDto : BaseDto
    {
		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; OnPropertyChanged(); }
		}

	}
}
