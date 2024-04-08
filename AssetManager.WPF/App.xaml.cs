using AssetManager.WPF.Common;
using AssetManager.WPF.Service;
using AssetManager.WPF.Service.Base;
using AssetManager.WPF.Service.IService;
using AssetManager.WPF.ViewModels;
using AssetManager.WPF.ViewModels.Dialogs;
using AssetManager.WPF.Views;
using AssetManager.WPF.Views.Dialogs;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;

namespace AssetManager.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(new HttpRestClient("https://localhost:7273/"));
            containerRegistry.Register<IProjectService, ProjectService>();
            containerRegistry.Register<IPlatformService, PlatformService>();
            containerRegistry.Register<IAssetPackageService, AssetPackageService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();

            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ProjectMgrView, ProjectMgrViewModel>();
            containerRegistry.RegisterForNavigation<PlatformView, PlatformViewModel>();
            containerRegistry.RegisterForNavigation<AddProjectView, AddProjectViewModel>();
            containerRegistry.RegisterForNavigation<AddVersionView, AddVersionViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            
            SplashScreen splashScreen = new SplashScreen("/Resources/Images/splashscreen.png");

            splashScreen.Show(false);

            splashScreen.Close(new TimeSpan(0, 0, 1));

            base.OnStartup(e);
        }
    }

}
