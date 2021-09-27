using ProjectManagment.DataAccess;
using ProjectManagment.Models;
using ProjectManagment.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ProjectManagment.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        public TasksViewModel(SessionContext context)
        {
            _context = context;
            _dataFactory = new MySqlDataFactory();
            _tasks = new ObservableCollection<Task>(_dataFactory.Tasks.GetTasksByUserId(_context.User.Id));
            TasksInProgress = new ObservableCollection<Task>(_tasks.Where(task => task.IsDone == false));
            TasksDone = new ObservableCollection<Task>(_tasks.Where(task => task.IsDone == true));
            _tasksInProgressHeader = $"{Resources.ResourceManager.GetString("InProgress")} ({TasksInProgress.Count})";
            _tasksDoneHeader = $"{Resources.ResourceManager.GetString("Done")} ({TasksDone.Count}/{_tasks.Count})";
            CompleteTaskCommand = new AnotherCommandImplementation(CompleteTask);
            UncompleteTaskCommand = new AnotherCommandImplementation(UncompleteTask);
        }
        private SessionContext _context { get; set; }
        private MySqlDataFactory _dataFactory;
        private ObservableCollection<Task> _tasks;
        public ICommand CompleteTaskCommand { get; }
        public ICommand UncompleteTaskCommand { get; }
        public ObservableCollection<Task> TasksInProgress { get; }
        public ObservableCollection<Task> TasksDone { get; }
        private string _tasksInProgressHeader;
        public string TasksInProgressHeader
        {
            get => _tasksInProgressHeader;
            set
            {
                _tasksInProgressHeader = value;
                OnPropertyChanged("TasksInProgressHeader");
            }
        }
        private string _tasksDoneHeader;
        public string TasksDoneHeader
        {
            get => _tasksDoneHeader;
            set
            {
                _tasksDoneHeader = value;
                OnPropertyChanged("TasksDoneHeader");
            }
        }


        // DELETE IF NOT NEEDED
        public Task SelectedTask { get; set; }
        private void CompleteTask(object o)
        {
            Task completedTask = _tasks.Where(t => t.Id == Int32.Parse(o.ToString())).First();
            if(completedTask != null)
            {
                TasksInProgress.Remove(completedTask);
                TasksDone.Add(completedTask);
                UpdateHeaders();
                _dataFactory.Tasks.CompleteTask(completedTask.Id);
            }
        }

        private void UncompleteTask(object o)
        {
            Task uncompletedTask = _tasks.Where(t => t.Id == Int32.Parse(o.ToString())).First();
            if(uncompletedTask != null)
            {
                TasksInProgress.Add(uncompletedTask);
                TasksDone.Remove(uncompletedTask);
                UpdateHeaders();
                _dataFactory.Tasks.UncompleteTask(uncompletedTask.Id);
            }
        }

        private void UpdateHeaders()
        {
            TasksInProgressHeader = $"{Resources.ResourceManager.GetString("InProgress")} ({TasksInProgress.Count})";
            TasksDoneHeader = $"{Resources.ResourceManager.GetString("Done")} ({TasksDone.Count}/{_tasks.Count})";
        }
    }
}
