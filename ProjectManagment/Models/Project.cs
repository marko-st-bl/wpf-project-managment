using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment.Models
{
    public class Project
    {
        public Project()
        {
            Tasks = new ObservableCollection<Task>();
        }
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public string Title { get; set; }
        public ObservableCollection<Task> Tasks { get; set; }
    }
}
