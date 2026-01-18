using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Desktop;
using Desktop.Repository;
using Todo.Entities;
using Desktop.View; 

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
            if (UserRepository.CurrentUser != null)
            {
                foreach (var t in TaskRepository.GetTasksForUser(UserRepository.CurrentUser.Id).Where(t => !t.IsDone))
                    Tasks.Add(t);
            }
            SelectedTask = Tasks.FirstOrDefault();
        }

        public MainWindow()
        {
            InitializeComponent();
            var userId = UserRepository.CurrentUser?.Id ?? Guid.Empty;
            Tasks = new ObservableCollection<TaskModel>(
                TaskRepository.GetTasksForUser(userId).Where(t => !t.IsDone)
            );
            SelectedTask = Tasks.FirstOrDefault();
            DataContext = this;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask == null) return;
            SelectedTask.IsDone = true;
            TaskRepository.MarkAsDone(SelectedTask);
            Tasks.Remove(SelectedTask);
            SelectedTask = Tasks.FirstOrDefault();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask == null) return;
            int index = Tasks.IndexOf(SelectedTask);
            TaskRepository.RemoveTask(SelectedTask);
            Tasks.Remove(SelectedTask);

            if (Tasks.Count > 0)
            {
                if (index >= Tasks.Count) index = Tasks.Count - 1;
                SelectedTask = Tasks[index];
            }
            else
            {
                SelectedTask = null;
            }
        }

        private void History_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HistoryPage historyPage = new HistoryPage();
            Window window = new Window
            {
                Title = "История задач",
                Content = historyPage,
                Width = 1200,
                Height = 750,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.Show();

            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTaskPage addTaskPage = new AddTaskPage();
            Window window = new Window
            {
                Title = "Новая задача",
                Content = addTaskPage,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();

            RefreshTasks();
        }
    }
}
