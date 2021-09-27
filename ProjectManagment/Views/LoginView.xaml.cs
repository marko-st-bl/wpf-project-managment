using ProjectManagment.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace ProjectManagment.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {


        public LoginView()
        {
            DataContext = new LoginViewModel();
            InitializeComponent();
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }
    }
}
