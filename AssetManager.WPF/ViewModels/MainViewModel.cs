using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace AssetManager.WPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private WindowState windowState;
        private string windowSizeIcon = "WindowRestore";

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

        public DelegateCommand MinWindowCommand { get; private set; }
        public DelegateCommand SetSizeWindowCommand { get; private set; }
        public DelegateCommand CloseWindowCommand { get; private set; }

        public MainViewModel()
        {
            MinWindowCommand = new DelegateCommand(MinWindow);
            SetSizeWindowCommand = new DelegateCommand(SetSizeWindow);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
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
    }
}
