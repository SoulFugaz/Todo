using System;
using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;
using Todo.Entities;
using TodoDesktop;

namespace Desktop
{
    public partial class AddTaskWindow : Window
    {
        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(CategoryBox.Text))
            {
                MessageBox.Show("Введите категорию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DatePickerTask.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (TimeBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите время!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string timeStr = ((ComboBoxItem)TimeBox.SelectedItem).Content.ToString()!;
            DateTime date = DatePickerTask.SelectedDate.Value;

            if (!TimeSpan.TryParse(timeStr, out TimeSpan time))
            {
                MessageBox.Show("Ошибка времени!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fullDateTime = date.Date + time;

            // Создаём задачу
            TaskModel task = new TaskModel
            {
                Title = TitleBox.Text.Trim(),
                Category = CategoryBox.Text.Trim(),
                Description = DescriptionBox.Text.Trim(),
                Date = fullDateTime,
                IsDone = false,
                OwnerId = UserRepository.CurrentUser!.Id
            };

            // Сохраняем задачу в репозиторий
            TaskRepository.AddTask(task);

            MessageBox.Show("Задача успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // === ЛОГИКА ОТКРЫТИЯ И ОБНОВЛЕНИЯ MAINWINDOW ===

            MainWindow? main = null;

            // Ищем — может MainWindow уже открыт
            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainWindow m)
                {
                    main = m;
                    break;
                }
            }

            // Если MainWindow нет — создаём новый
            if (main == null)
            {
                main = new MainWindow();
                main.Show();
            }

            // Обновляем задачи (лучше, чем просто Add)
            if (main.Tasks != null)
            {
                main.RefreshTasks();
                main.SelectedTask = task;
            }

            // Закрываем окно MainEmpty, если оно открыто
            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainEmpty)
                {
                    w.Close();
                    break;
                }
            }

            // Закрываем добавление
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
