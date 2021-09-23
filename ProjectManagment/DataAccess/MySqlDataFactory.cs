using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment.DataAccess
{
    public class MySqlDataFactory
    {
        private MySqlUser _mySqlUser;
        private MySqlSettings _mySqlSettings;
        private MySqlProject _mySqlProject;
        private MySqlTask _mySqlTask;

        public MySqlUser Users
        {
            get
            {
                if (_mySqlUser == null)
                {
                    _mySqlUser = new MySqlUser();
                }
                return _mySqlUser;
            }
        }

        public MySqlSettings Settings
        {
            get
            {
                if(_mySqlSettings == null)
                {
                    _mySqlSettings = new MySqlSettings();
                }
                return _mySqlSettings;
            }
        }

        public MySqlProject Projects
        {
            get
            {
                if(_mySqlProject == null)
                {
                    _mySqlProject = new MySqlProject();
                }
                return _mySqlProject;
            }
        }

        public MySqlTask Tasks
        {
            get
            {
                if(_mySqlTask == null)
                {
                    _mySqlTask = new MySqlTask();
                }
                return _mySqlTask;
            }
        }
    }
}
