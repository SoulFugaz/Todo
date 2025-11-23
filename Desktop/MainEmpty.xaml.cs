using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TodoDesktop;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для MainEmpty.xaml
    /// </summary>
    public partial class MainEmpty : Window
    {
        public MainEmpty()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно создания задачи
            var addWindow = new AddTaskWindow();
            addWindow.Owner = this;

            // Ждём результата
            bool? result = addWindow.ShowDialog();

            // Если задача успешно создана — открываем MainWindow
            if (result == true)
            {
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

    }
}
