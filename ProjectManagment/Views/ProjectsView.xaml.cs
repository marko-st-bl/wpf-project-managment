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
    }
}
