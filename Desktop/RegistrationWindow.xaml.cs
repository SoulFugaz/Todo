using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;
using Todo.Entities;

namespace Desktop
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) { }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = UsernameBox.Text?.Trim() ?? "";
            string email = EmailBox.Text?.Trim() ?? "";
            string password1 = PasswordBox1.Password;
            string password2 = PasswordBox2.Password;

            if (!InputValidator.ValidateName(name))
            {
                MessageBox.Show("Имя должно содержать не менее трёх символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!InputValidator.ValidateEmail(email))
            {
                MessageBox.Show("Введите корректный адрес почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!InputValidator.ValidatePassword(password1))
            {
                MessageBox.Show("Пароль должен содержать не менее шести символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password1 != password2)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new UserModel
            {
                Name = name,
                Email = email,
                Password = password1
            };

            if (!UserRepository.Register(newUser, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
