using System.Windows;
using System.Windows.Controls;

namespace Desktop
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Кнопка "Регистрация" (переход на окно регистрации)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        // Текст почты
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Можно добавить визуальную подсветку, если нужно
        }

        // Кнопка "Войти"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox1.Password.Trim();

            // Проверяем поля
            if (!InputValidator.ValidateEmail(email))
            {
                MessageBox.Show("Введите корректную почту (пример: example@mail.ru)",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!InputValidator.ValidatePassword(password))
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Если всё верно — переход на MainEmpty
            MainEmpty mainWindow = new MainEmpty();
            mainWindow.Show();
            this.Close();
        }
    }
}