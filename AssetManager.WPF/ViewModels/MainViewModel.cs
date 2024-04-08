using AssetManager.WPF.Common.Models;
using AssetManager.WPF.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;

namespace AssetManager.WPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private WindowState windowState;
        private string windowSizeIcon = "WindowRestore";
        private ObservableCollection<MenuBar>? menuBars;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal? journal;

        public WindowState CustomWindowState
        {
            get { return windowState; }
            set { windowState = value; RaisePropertyChanged(); }
        }
        public string WindowSizeIcon
        {
            get { return windowSizeIcon; }
            set { windowSizeIcon = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<MenuBar>? MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand MinWindowCommand { get; private set; }
        public DelegateCommand SetSizeWindowCommand { get; private set; }
        public DelegateCommand CloseWindowCommand { get; private set; }

        public MainViewModel(IRegionManager regionManager)
        {
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            MinWindowCommand = new DelegateCommand(MinWindow);
            SetSizeWindowCommand = new DelegateCommand(SetSizeWindow);
            CloseWindowCommand = new DelegateCommand(CloseWindow);

            MenuBars = new ObservableCollection<MenuBar>();

            this.regionManager = regionManager;

            CreateMenubar();
        }

        /// <summary>
        /// 窗口最小化
        /// </summary>
        private void MinWindow()
        {
            CustomWindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 设置窗口最大化和正常
        /// </summary>
        private void SetSizeWindow()
        {
            if (CustomWindowState == WindowState.Maximized)
            {
                CustomWindowState = WindowState.Normal;
                WindowSizeIcon = "WindowRestore";
            }
            else
            {
                CustomWindowState = WindowState.Maximized;
                WindowSizeIcon = "WindowMaximize";
            }
            
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        private void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="bar"></param>
        private void Navigate(MenuBar bar)
        {
            if (bar == null || string.IsNullOrEmpty(bar.NameSpace))
            {
                return;
            }

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
        }

        private void CreateMenubar()
        {
            if (MenuBars == null)
            {
                MenuBars = new ObservableCollection<MenuBar>();
            }

            MenuBars.Add(new MenuBar() { Icon = "HomeOutline", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "ListBoxOutline", Title = "项目管理", NameSpace = "ProjectMgrView" });
            MenuBars.Add(new MenuBar() { Icon = "Laptop", Title = "平台管理", NameSpace = "PlatformView" });
            MenuBars.Add(new MenuBar() { Icon = "CogOutline", Title = "系统设置", NameSpace = "SettingsView" });
        }
    }
}
