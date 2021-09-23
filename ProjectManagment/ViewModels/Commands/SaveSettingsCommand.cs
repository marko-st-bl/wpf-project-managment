using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagment.ViewModels.Commands
{
    public class SaveSettingsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;

        public SaveSettingsCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
