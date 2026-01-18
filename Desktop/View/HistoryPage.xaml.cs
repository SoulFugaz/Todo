using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Desktop.Repository;
using Todo.Entities;
using TodoDesktop; 

namespace Desktop.View
{
    public partial class HistoryPage : Page
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }
        public TaskModel SelectedTask { get; set; }
        public string CurrentUserName => UserRepository.CurrentUser?.Name ?? "";

        public HistoryPage()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskModel>(
                TaskRepository
                    .GetTasksForUser(UserRepository.CurrentUser!.Id)
                    .Where(t => t.IsDone)
            );
            SelectedTask = Tasks.FirstOrDefault();
            DataContext = this;
        }

        private void BackToMain_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new MainWindow().Show();

            Window.GetWindow(this)?.Close();
        }
    }
}
