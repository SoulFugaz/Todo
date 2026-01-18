using System;
using System.Windows;
using System.Windows.Controls;
using Desktop.Repository;
using Todo.Entities;

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
            if (string.IsNullOrWhiteSpace(TitleBox.Text) ||
                string.IsNullOrWhiteSpace(CategoryBox.Text) ||
                DatePickerTask.SelectedDate == null ||
                TimeBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            var time = TimeSpan.Parse(((ComboBoxItem)TimeBox.SelectedItem).Content.ToString()!);

            var task = new TaskModel
            {
                Title = TitleBox.Text.Trim(),
                Category = CategoryBox.Text.Trim(),
                Description = DescriptionBox.Text.Trim(),
                Date = DatePickerTask.SelectedDate.Value.Date + time,
                OwnerId = UserRepository.CurrentUser!.Id
            };

            TaskRepository.AddTask(task);

            NavigationService.Navigate(new MainEmptyPage());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
