using Prism.Commands;
using Prism.Services.Dialogs;

namespace AssetManager.WPF.Common
{
    public interface IDialogHostAware
    {
        public string DialogHostName { get; set; }

        void OnDialogOpend(IDialogParameters parameters);

        DelegateCommand SaveCommand { get; set; }

        DelegateCommand CancelCommand { get; set; }
    }
}
