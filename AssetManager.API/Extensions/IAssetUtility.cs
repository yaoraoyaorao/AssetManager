namespace AssetManager.API.Extensions
{
    public interface IAssetUtility
    {
        /// <summary>
        /// 数据文件夹路径
        /// </summary>
        public string dataPath { get; set; }

        /// <summary>
        /// 根目录
        /// </summary>
        public string rootPath { get; set; }

        public void CreateOrUpdateFolder(string newProjectName, string updateProjectName = "");

        public void CreateFolder(string folderPath);

        public void DeleteFolder(string folderName);

        public void CreateFolders(params string[] folderNames);
    }
}
