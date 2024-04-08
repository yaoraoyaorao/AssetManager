using AssetManager.WPF.Extensions;
using Prism.Events;
using System.Windows;
using System.Windows.Input;

namespace AssetManager.WPF.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator aggregator)
        {
            InitializeComponent();

            WindowColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            };

            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };

            //注册等待消息窗口
            aggregator.Register(arg =>
            {
                MainDialogHost.IsOpen = arg.IsOpen;

                if (MainDialogHost.IsOpen)
                {
                    MainDialogHost.DialogContent = new ProgressView();
                }
            });

            //注册提示消息
            aggregator.RegisterMessage(arg =>
            {
                snackbar.MessageQueue.Enqueue(arg.Message);
            });

        }
    }
}
