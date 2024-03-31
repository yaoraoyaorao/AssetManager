using AssetManager.WPF.Views;
using Prism.Ioc;
using Prism.Unity;
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
           
        }
    }

}
