using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Desktop.Repository;
using Todo.Entities;
using Desktop; 

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
            string name = UsernameBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string pass1 = PasswordBox1.Password;
            string pass2 = PasswordBox2.Password;


            if (!InputValidator.ValidateName(name))
            {
                MessageBox.Show("Имя пользователя должно содержать минимум 2 символа.",
                                "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (!InputValidator.ValidateEmail(email))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.",
                                "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (!InputValidator.ValidatePassword(pass1))
            {
                MessageBox.Show("Пароль должен быть не менее 6 символов.",
                                "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (pass1 != pass2)
            {
                MessageBox.Show("Пароли не совпадают!",
                                "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            var user = new UserModel
            {
                Name = name,
                Email = email,
                Password = pass1
            };

            if (!UserRepository.Register(user, out var error))
            {
                MessageBox.Show(error, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Регистрация прошла успешно!", "Успех");

            NavigationService.Navigate(new LoginPage());
        }
    }
}
