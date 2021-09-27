using ProjectManagment.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using ProjectManagment.Properties;

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Snackbar.MessageQueue.Enqueue(
                ProjectManagment.Properties.Resources.ResourceManager.GetString("SettingsSaved"),
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(5)
                );
        }
    }
}
