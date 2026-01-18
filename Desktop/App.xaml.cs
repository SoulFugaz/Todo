using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Desktop.View;

namespace Desktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new Window
            {
                Title = "Todo App",
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize
            };


            var frame = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                Focusable = false,
                BorderThickness = new Thickness(0)
            };

            frame.Navigate(new LoginPage());

            window.Content = frame;
            window.Show();
        }
    }
}
