using ProjectManagment.Models;
using ProjectManagment.Properties;
using ProjectManagment.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment.ViewModels
{
    class MemberViewModel : ViewModelBase
    {
        public MemberViewModel(SessionContext context)
        {
            _user = context.User;

            MenuItems = new ObservableCollection<MenuItem>(new[]
            {
                new MenuItem(
                    Resources.ResourceManager.GetString("MyTasks"),
                    "CalendarCheckOutline",
                    typeof(TasksView),
                    context),
                new MenuItem(
                    Resources.ResourceManager.GetString("Settings"),
                    "Gear",
                    typeof(SettingsView),
                    context)
            });
        }

        private User _user;
        public string FullName { get => $"{_user.FirstName} {_user.LastName}"; }
        public string Initials
        {
            get => $"{_user.FirstName.Substring(0, 1)}{_user.LastName.Substring(0, 1)}";
        }
        public string Type { get => _user.Type; }
        private MenuItem _selectedItem;
        private int _selectedIndex;

        public ObservableCollection<MenuItem> MenuItems { get; }
        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
    }
}
