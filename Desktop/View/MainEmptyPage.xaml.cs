using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Desktop.View
{
    public partial class MainEmptyPage : Page
    {
        public MainEmptyPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddTaskPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
