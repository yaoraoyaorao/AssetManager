using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AssetManager.WPF.Service.IService;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace AssetManager.WPF.ViewModels
{
    public class ProjectMgrViewModel: NavigationViewModel
    {
        private ObservableCollection<ProjectDto> projects;
        private bool isRightDrawerOpen;
        private ProjectDto currentProject;
        private readonly IProjectService service;

        public ObservableCollection<ProjectDto> Projects
        {
            get { return projects; }
            set { projects = value; RaisePropertyChanged(); }
        }

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        public ProjectDto CurrentProject
        {
            get { return currentProject; }
            set { currentProject = value; RaisePropertyChanged(); }
        }

        public ProjectMgrViewModel(IProjectService service, IContainerProvider container) : base(container)
        {
            Projects = new ObservableCollection<ProjectDto>();

            this.service = service;
        }

        private async void GetProjectData()
        {
            try
            {
                UpdateLoading(true);
                var response = await service.GetAllAsync(new QueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 10,
                    Search = ""
                });

                if (response.Code == 200)
                {
                    Projects.Clear();

                    foreach (var item in response.Data.Items)
                    {
                        Projects.Add(item);
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                UpdateLoading(false);
            }
           
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            GetProjectData();
        }
    }
}
