using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Desktop.Repository;
using TodoDesktop;

namespace Desktop.View
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!UserRepository.Login(EmailBox.Text, PasswordBox1.Password))
            {
                MessageBox.Show("Неверные данные");
                return;
            }

            var userId = UserRepository.CurrentUser?.Id;
            if (userId == null) return;

            bool hasTasks = TaskRepository.GetTasksForUser(userId.Value).Any();

            if (hasTasks)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                Window.GetWindow(this)?.Close();
            }
            else
            {
                NavigationService.Navigate(new MainEmptyPage());
            }
        }
    }
}
