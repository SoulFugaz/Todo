using System.Windows;
using System.Windows.Controls;

namespace Desktop
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        // "Назад" — возвращаемся в LoginWindow
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        // Эти обработчики можно оставить пустыми или использовать для подсказок/валидации "на лету"
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) { }

        // "Зарегистрироваться" — основной обработчик
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = UsernameBox.Text?.Trim();
            string email = EmailBox.Text?.Trim();
            string password1 = PasswordBox1.Password;
            string password2 = PasswordBox2.Password;

            // Проверка имени
            if (!InputValidator.ValidateName(name))
            {
                MessageBox.Show("Имя должно содержать не менее трёх символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка почты
            if (!InputValidator.ValidateEmail(email))
            {
                MessageBox.Show("Введите корректный адрес почты (пример: example@mail.com).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка пароля
            if (!InputValidator.ValidatePassword(password1))
            {
                MessageBox.Show("Пароль должен содержать не менее шести символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка совпадения паролей
            if (password1 != password2)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // При желании — здесь можно сохранить данные пользователя в бд / файл
            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Переходим на окно входа
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}