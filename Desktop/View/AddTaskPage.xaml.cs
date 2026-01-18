using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Desktop.Repository;
using Todo.Entities;
using TodoDesktop;

namespace Desktop.View
{
    public partial class AddTaskPage : Page
    {
        public AddTaskPage()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбраны ли значения в ComboBox (CategoryBox и TimeBox)
            if (string.IsNullOrWhiteSpace(TitleBox.Text) ||
                CategoryBox.SelectedItem == null ||  // <-- Проверка выбора категории
                DatePickerTask.SelectedDate == null ||
                TimeBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            var time = TimeSpan.Parse(((ComboBoxItem)TimeBox.SelectedItem).Content.ToString()!);

            // Получаем текст из выбранного элемента ComboBox
            var category = ((ComboBoxItem)CategoryBox.SelectedItem).Content.ToString();

            var task = new TaskModel
            {
                Title = TitleBox.Text.Trim(),
                Category = category!, // <-- Используем выбранную категорию
                Description = DescriptionBox.Text.Trim(),
                Date = DatePickerTask.SelectedDate.Value.Date + time,
                OwnerId = UserRepository.CurrentUser!.Id
            };

            TaskRepository.AddTask(task);

            // Логика закрытия (как делали ранее)
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                Window.GetWindow(this)?.Close();
            }
            else
            {
                new MainWindow().Show();
                Window.GetWindow(this)?.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                Window.GetWindow(this)?.Close();
            }
        }
    }
}
