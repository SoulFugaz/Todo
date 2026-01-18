using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!UserRepository.Login(EmailBox.Text, PasswordBox1.Password))
            {
                MessageBox.Show("Неверные данные");
                return;
            }

            bool hasTasks = TaskRepository
                .GetTasksForUser(UserRepository.CurrentUser!.Id)
                .Any();

            NavigationService.Navigate(
                hasTasks ? new MainEmptyPage() : new MainEmptyPage()
            );
        }
    }
}
