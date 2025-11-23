using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;
using TodoDesktop;

namespace Desktop
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Кнопка "Регистрация"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow reg = new RegistrationWindow();
            reg.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }

        // Кнопка "Войти"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox1.Password.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Введите Email!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Авторизация
            bool success = UserRepository.Login(email, password);

            if (!success)
            {
                MessageBox.Show("Неверный Email или пароль!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // --- НОВАЯ ЛОГИКА: проверка есть ли задачи у пользователя ---
            var userId = UserRepository.CurrentUser!.Id;
            bool hasTasks = TaskRepository.GetTasksForUser(userId).Any();

            if (!hasTasks)
            {
                // Нет задач → открываем MainEmpty
                MainEmpty empty = new MainEmpty();
                empty.Show();
                this.Close();
                return;
            }
            else
            {
                // Есть задачи → открываем MainWindow
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
    }
}
