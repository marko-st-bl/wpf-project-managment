using ProjectManagment.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManagment.Views
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksView : UserControl
    {
        public TasksView(SessionContext context)
        {
            _viewModel = new TasksViewModel(context);
            DataContext = _viewModel;
            InitializeComponent();
        }

        private readonly TasksViewModel _viewModel;

        private void Complete_Button_Click(object sender, RoutedEventArgs e)
        {
            sender.GetType();
            var t = _viewModel.SelectedTask.Title;
        }
    }
}
