using AssetManager.WPF.Common;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace AssetManager.WPF.ViewModels
{
    public class MsgViewModel : BindableBase, IDialogHostAware
    {
        private string title;
        private string content;

        public string DialogHostName { get; set; } = "Root";
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }
        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }

        public MsgViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters();
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");

            if (parameters.ContainsKey("Content"))
                Content = parameters.GetValue<string>("Content");
        }
    }
}
