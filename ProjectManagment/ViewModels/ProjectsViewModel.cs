﻿using MaterialDesignThemes.Wpf;
using ProjectManagment.DataAccess;
using ProjectManagment.Models;
using ProjectManagment.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManagment.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        public ProjectsViewModel(SessionContext context)
        {
            _context = context;
            _dataFactory = new MySqlDataFactory();
            Projects = new ObservableCollection<Project>();
            Users = new ObservableCollection<User>();
            RunExtendedCommand = new AnotherCommandImplementation(ExecuteRunExtendedDialog);
            RemoveSelectedProjectCommand = new AnotherCommandImplementation(ExecuteRunPromtDialog);
            LoadProjects();
            LoadUsers();
        }
        public ICommand RunExtendedCommand { get; }
        public ICommand RemoveSelectedProjectCommand { get; }
        private SessionContext _context { get; set; }
        private MySqlDataFactory _dataFactory;
        public ObservableCollection<Project> Projects { get; set; }
        public ObservableCollection<User> Users { get; set; }
        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private void LoadProjects()
        {
            List<Project> projects = _dataFactory.Projects.GetProjectsByManagerId(_context.User.Id);
            projects.ForEach(p => {
                p.Tasks = _dataFactory.Tasks.GetTasksByProjectId(p.Id);
                Projects.Add(p);
                });
        }
        private void LoadUsers()
        {
            List<User> users = _dataFactory.Users.GetAllNonManagerUsers();
            users.ForEach(user => Users.Add(user));
        }
        private async void ExecuteRunExtendedDialog(object o)
        {
            var view = new AddProjectDialog();

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog");
            if(result is string && result != "")
            {
                Project newProject = new Project() { Title = (string)result, ManagerId = _context.User.Id };
                _dataFactory.Projects.AddProject(newProject);
                Projects.Add(newProject);
            }

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void ExecuteRunPromtDialog(object o)
        {
            var view = new PromtDialog();
            var result = await DialogHost.Show(view, "RootDialog");
            if((bool)result)
            {
                var selectedProject = SelectedItem as Project;
                if(selectedProject != null)
                {
                    _dataFactory.Projects.DeleteProjectById(selectedProject.Id);
                    Projects.Remove(selectedProject);
                }
            }
        }
    }
}