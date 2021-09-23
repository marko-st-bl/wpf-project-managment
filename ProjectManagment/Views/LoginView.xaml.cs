using ProjectManagment.DataAccess;
using ProjectManagment.Models;
using ProjectManagment.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ProjectManagment.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {


        public LoginView()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr");
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authenticate(usernameTextBox.Text, passwordTextBox.Password);
        }

        MySqlDataFactory factory;
        private void Authenticate(string username, string password)
        {
            User user;
            user = new MySqlDataFactory().Users.GetUserByUsername(username);
            if(user != null)
            {
                if(user.Password == password)
                {
                    if(user.Type == "manager")
                    {
                        ManagerView mw = new ManagerView(user);
                        mw.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid login", "Failed");
                }
            }
            else
            {
                MessageBox.Show("Invalid login", "Failed");
            }
        }
    }
}
