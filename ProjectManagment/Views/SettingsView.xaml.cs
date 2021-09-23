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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView(SessionContext context)
        {
            DataContext = new SettingsViewModel(context);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr");
           
        }

        
    }
}
