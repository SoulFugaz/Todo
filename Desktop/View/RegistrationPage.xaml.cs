using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;
using Todo.Entities;

namespace Desktop.View
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = new UserModel
            {
                Name = UsernameBox.Text,
                Email = EmailBox.Text,
                Password = PasswordBox1.Password
            };

            if (!UserRepository.Register(user, out var error))
            {
                MessageBox.Show(error);
                return;
            }

            NavigationService.Navigate(new LoginPage());
        }
    }
}
