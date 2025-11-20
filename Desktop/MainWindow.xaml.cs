using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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

        public string CurrentUserName => "Alex";

        public MainWindow()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<TaskModel>()
            {
                new TaskModel { Title="Go fishing with Stephen", Description="Lorem ipsum...", Date = DateTime.Now },
                new TaskModel { Title="Read the book Zlatan", Description="Lorem ipsum...", Date = DateTime.Now },
                new TaskModel { Title="Meet according with design team", Description="Lorem ipsum...", Date = DateTime.Now },
                new TaskModel { Title="Clean the house", Description="Lorem ipsum...", Date = DateTime.Now },
                new TaskModel { Title="Go to gym", Description="Lorem ipsum...", Date = DateTime.Now }
            };

            SelectedTask = Tasks[0];
            DataContext = this;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask != null)
            {
                SelectedTask.IsDone = true;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTask == null)
                return;

            int index = Tasks.IndexOf(SelectedTask);

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
