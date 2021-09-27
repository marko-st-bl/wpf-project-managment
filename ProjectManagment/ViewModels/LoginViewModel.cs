using ProjectManagment.DataAccess;
using ProjectManagment.Models;
using ProjectManagment.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagment.ViewModels
{
    class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
            LoginCommand = new AnotherCommandImplementation(Login);
        }
        public String Username { get; set; }
        public String Password { get; set; }
        public SecureString SecurePassword { private get; set; }
        public ICommand LoginCommand { get; }
        private void Login(object o)
        {
            User user = new MySqlDataFactory().Users.GetUserByUsername(Username);
            if (user != null)
            {
                if (user.Password == Password)
                {
                    if (user.Type == "manager")
                    {
                        ManagerView mw = new ManagerView(user);
                        mw.Show();
                        var window = o as Window;
                        window.Close();
                    }
                    else
                    {
                        MemberView mw = new MemberView(user);
                        mw.Show();
                        var window = o as Window;
                        window.Close();
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
