using ExercicePP.Models;
using ExercicePP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExercicePP.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
       public  MainWindowViewModel() 
        { 
            toDoListViewModel = new ToDoListViewModel(this);
            _selectedViewModel = toDoListViewModel;
        }
        private ToDoListViewModel toDoListViewModel;

        private ViewModelBase _selectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// switching from ToDoListView to TaskView
        /// </summary>
        /// <param name="task"></param>
        public void DisplayTaskView(Task_ task)
        {
            SelectedViewModel = new TaskViewModel(task, this);
        }

        /// <summary>
        /// saving Task and switching from TaskView to ToDoListView 
        /// </summary>
        /// <param name="task"></param>
        public void SaveTask(Task_ task)
        {
            toDoListViewModel.SaveTask(task);
            SelectedViewModel = toDoListViewModel;
        }

        /// <summary>
        /// Only switching from TaskView to ToDoListView 
        /// </summary>
        /// <param name="task"></param>
        public void CancelSave()
        {
            SelectedViewModel = toDoListViewModel;
        }





    }
}
