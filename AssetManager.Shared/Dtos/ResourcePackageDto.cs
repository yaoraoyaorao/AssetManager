namespace AssetManager.Shared.Dtos
{
    public class ResourcePackageDto : BaseDto
    {
        private int projectID;
        private int platformID;
        private string version;
        private string downloadLinks;
        private AuditStatus auditStatus;
        private ProjectItemDto projectItem;
        private PlatformDto platform;

        public int ProjectID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
                OnPropertyChanged();
            }
        }

        public int PlatformID
        {
            get
            {
                return platformID;
            }
            set
            {
                platformID = value;
                OnPropertyChanged();
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged();
            }
        }

        public string DownloadLinks
        {
            get
            {
                return downloadLinks;
            }
            set
            {
                downloadLinks = value;
                OnPropertyChanged();
            }
        }

        public AuditStatus AuditStatus
        {
            get
            {
                return auditStatus;
            }
            set
            {
                auditStatus = value;
                OnPropertyChanged();
            }
        }

        public ProjectItemDto ProjectItem
        {
            get
            {
                return projectItem;
            }
            set
            {
                projectItem = value;
                OnPropertyChanged();
            }
        }

        public PlatformDto Platform
        {
            get
            {
                return platform;
            }
            set
            {
                platform = value;
                OnPropertyChanged();
            }
        }
    }

    public enum AuditStatus
    {
        Pending,
        Approved,
        Rejected,
    }
}
