﻿using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows;

namespace AssetManager.WPF.Common
{
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器中取出弹出窗口的实例
            var content = containerExtension.Resolve<object>(name);

            if (!(content is FrameworkElement dialogContent))
            {
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");
            }

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
            {
                ViewModelLocator.SetAutoWireViewModel(view, true);
            }

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
            {
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");
            }

            viewModel.DialogHostName = dialogHostName;

            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);
                }

                eventArgs.Session.UpdateContent(content);
            };

            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
        }
    }
}
