namespace AssetManager.Shared.Dtos
{
    public class AssetPackageDto : BaseDto
    {
        private int auditStatus;
        private int max;
        private int min;
        private int patch;
        


        public int AuditStatus
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

        public int Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
                OnPropertyChanged();
            }
        }

        public int Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
                OnPropertyChanged();
            }
        }

        public int Patch
        {
            get
            {
                return patch;
            }
            set
            {
                patch = value;
                OnPropertyChanged();
            }
        }
    }
}
