using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Desktop.Repository;
using Todo.Entities;

namespace TodoDesktop
{
    public partial class HistoryWindow : Window
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }
        public TaskModel SelectedTask { get; set; }

        public string CurrentUserName => UserRepository.CurrentUser?.Name ?? "Unknown";

        public HistoryWindow()
        {
            InitializeComponent();

            var userId = UserRepository.CurrentUser!.Id;

            Tasks = new ObservableCollection<TaskModel>(
                TaskRepository.GetTasksForUser(userId).Where(t => t.IsDone)
            );

            SelectedTask = Tasks.FirstOrDefault();

            DataContext = this;
        }

        private void BackToMain_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
