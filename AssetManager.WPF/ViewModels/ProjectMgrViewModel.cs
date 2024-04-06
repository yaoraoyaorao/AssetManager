using AssetManager.Shared.Dtos;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace AssetManager.WPF.ViewModels
{
    public class ProjectMgrViewModel:BindableBase
    {
        private ObservableCollection<ProjectDto> projects;

        public ObservableCollection<ProjectDto> Projects
        {
            get { return projects; }
            set { projects = value; RaisePropertyChanged(); }
        }

        public ProjectMgrViewModel()
        {
            Projects = new ObservableCollection<ProjectDto>();

            GetProjectData();
        }

        private void GetProjectData()
        {
            //测试
            for (int i = 0; i < 10; i++)
            {
                Projects.Add(new ProjectDto()
                {
                    Id = i,
                    Name = "项目:" + i,
                    Description = "项目描述:" + i,
                    Guid = Guid.NewGuid().ToString()
                });
            }
        }
    }
}
