using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment.ViewModels
{
    public class SessionContext : ViewModelBase
    {
        public SessionContext()
        {

        }

        public User User { get; set; }
        public Settings Settings { get; set; }
    }
}
