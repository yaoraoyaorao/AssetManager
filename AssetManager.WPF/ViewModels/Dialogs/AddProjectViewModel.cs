using AssetManager.Shared.Dtos;
using AssetManager.WPF.Common;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace AssetManager.WPF.ViewModels.Dialogs
{
    public class AddProjectViewModel : BindableBase, IDialogHostAware
    {
        private ProjectDto project;

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public ProjectDto Project
        {
            get { return project; }
            set { project = value;RaisePropertyChanged(); }
        }

        public AddProjectViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                Project = parameters.GetValue<ProjectDto>("Value");
            }
            else
            {
                Project = new ProjectDto();
            }
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Project.Name))
            {
                return;
            }

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters
                {
                    { "Value", Project }
                };
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }
    }
}
