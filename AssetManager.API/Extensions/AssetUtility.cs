namespace AssetManager.API.Extensions
{
    public class AssetUtility : IAssetUtility
    {
        private string _dataPath;
        private string _rootPath;

        /// <summary>
        /// 数据文件夹路径
        /// </summary>
        public string dataPath
        {
            get
            {
                return _dataPath;
            }
            set
            {
                _dataPath = value;
            }
        }

        /// <summary>
        /// 根目录
        /// </summary>
        public string rootPath
        {
            get
            {
                return _rootPath;
            }
            set
            {
                _rootPath = value;
            }
        }

        public AssetUtility()
        {
            rootPath = AppDomain.CurrentDomain.BaseDirectory;
            dataPath = Path.Combine(rootPath, "Data");

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
        }

        /// <summary>
        /// 创建项目文件夹
        /// </summary>
        /// <param name="newProjectName"></param>
        public void CreateOrUpdateFolder(string newProjectName, string updateProjectName = "")
        {
            if (string.IsNullOrWhiteSpace(newProjectName))
            {
                throw new Exception("文件夹不能为空");
            }

            string newProjectPath = Path.Combine(dataPath, newProjectName);
            string updateProjectPath = Path.Combine(dataPath, updateProjectName);

            if (string.IsNullOrEmpty(updateProjectName))
            {
                CreateFolder(newProjectPath);
            }
            else
            {
                if (!Directory.Exists(newProjectPath))
                {
                    throw new Exception("初始文件夹不存在");
                }

                Directory.Move(newProjectPath, updateProjectPath);
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new Exception("文件不能为空");
            }

            string folderPath = Path.Combine(dataPath, folderName);

            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// 创建多个文件夹
        /// </summary>
        /// <param name="folderNames"></param>
        public void CreateFolders(params string[] folderNames)
        {
            string path = dataPath;
            
            foreach (var folderName in folderNames)
            {
                path = Path.Combine(path, folderName);
            }

            Directory.CreateDirectory(path);
        }
    }
}
