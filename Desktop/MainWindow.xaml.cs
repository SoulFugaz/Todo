using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Desktop;
using Desktop.Repository;
using Todo.Entities;

namespace TodoDesktop
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }

        private TaskModel _selectedTask;
        public TaskModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CurrentUserName => UserRepository.CurrentUser?.Name ?? "Unknown";

        public void RefreshTasks()
        {
            Tasks.Clear();

            foreach (var t in TaskRepository.GetTasksForUser(UserRepository.CurrentUser!.Id).Where(t => !t.IsDone))
                Tasks.Add(t);

            SelectedTask = Tasks.FirstOrDefault();
        }


        public MainWindow()
        {
            InitializeComponent();

            // ❗ Загружаем только задачи текущего пользователя
            var userId = UserRepository.CurrentUser!.Id;

            Tasks = new ObservableCollection<TaskModel>(
    TaskRepository.GetTasksForUser(userId).Where(t => !t.IsDone)
            );

            // Если задач нет – ничего не выделяем
            SelectedTask = Tasks.FirstOrDefault();

            DataContext = this;
        }

        // Отметить как выполненное
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask == null)
                return;

            SelectedTask.IsDone = true;
            TaskRepository.MarkAsDone(SelectedTask);

            // удаляем из активных
            Tasks.Remove(SelectedTask);

            // выбрать новую
            SelectedTask = Tasks.FirstOrDefault();
        }


        // Удалить задачу
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask == null)
                return;

            int index = Tasks.IndexOf(SelectedTask);

            // Удаляем и из UI, и из хранилища
            TaskRepository.RemoveTask(SelectedTask);
            Tasks.Remove(SelectedTask);

            if (Tasks.Count > 0)
            {
                if (index >= Tasks.Count)
                    index = Tasks.Count - 1;

                SelectedTask = Tasks[index];
            }
            else
            {
                SelectedTask = null;
            }
        }
        private void History_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HistoryWindow hw = new HistoryWindow();
            hw.Show();
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTask = new AddTaskWindow();
            addTask.ShowDialog();

            // После закрытия окна обновляем список задач
            RefreshTasks();
        }

    }
}
