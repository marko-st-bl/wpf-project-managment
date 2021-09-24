using ProjectManagment.DataAccess;
using ProjectManagment.Models;
using ProjectManagment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : UserControl
    {
        private readonly ProjectsViewModel _viewModel;
        public ProjectsView(SessionContext context)
        {
            _viewModel = new ProjectsViewModel(context);
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) => _viewModel.SelectedItem = e.NewValue;

        private void DataGridTasks_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dg = sender as DataGrid;
            Task task = dg.SelectedItem as Task;
            if(task != null)
            {
                Task newTask = task;

                TextBox tb = e.EditingElement as TextBox;
                var cb = e.EditingElement as CheckBox;
                var comboBox = e.EditingElement as ComboBox;
                
                if (tb != null)
                {
                    switch(e.Column.Header)
                    {
                        case("Title"):
                            task.Title = tb.Text;
                            break;
                        case("Description"):
                            task.Description = tb.Text;
                            break; 
                    }
                } 
                else if(cb != null)
                {
                    newTask.IsDone = (bool)cb.IsChecked;
                }
                else if(comboBox != null)
                {
                    var user = comboBox.SelectedItem as User;
                    if(user != null)
                    {
                        new MySqlDataFactory().Tasks.AssignTaskToUser(task.Id, (_viewModel.SelectedItem as Project).Id, user.Id);
                    }
                }
                new MySqlDataFactory().Tasks.UpdateTask(task);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task task = _viewModel.SelectedTask;
            if(task != null)
            {
                new MySqlDataFactory().Tasks.DeleteTask(task.Id);
                foreach(Project p in _viewModel.Projects)
                {
                    if(p.Tasks.Contains(task))
                    {
                        p.Tasks.Remove(task);
                        break;
                    }
                }
            }
        }
    }
}
