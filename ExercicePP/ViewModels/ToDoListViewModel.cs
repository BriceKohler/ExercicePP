using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ExercicePP.Models;
using ExercicePP.Utilities;

namespace ExercicePP.ViewModels
{
    internal class ToDoListViewModel : ViewModelBase
    {
        #region Constructor
        public ToDoListViewModel(MainWindowViewModel mainwindow)
        {
            Initialize = ImportDataAsync();
            mainWindowViewModel = mainwindow;
        }
        #endregion Constructor

        #region Variables
        private MainWindowViewModel mainWindowViewModel;
        private string filename = @".\Tasks.txt";
        #endregion Variables

        #region Properties
        private ObservableCollection<Task_> _tasks;
        public ObservableCollection<Task_> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        private Task_ _selectedtask;
        public Task_ SelectedTask
        {
            get
            {
                return _selectedtask;
            }
            set
            {
                _selectedtask = value;
                OnPropertyChanged();

            }
        }

        public ObservableCollection<Task_> FilteredTasks
        {
            get
            {



                var l_filteredtasks = Tasks.ToList();
                if (NameFilter != null) l_filteredtasks = l_filteredtasks.Where(t => t.TaskName.Contains(NameFilter)).ToList();
                if (RespFilter != "All") l_filteredtasks = l_filteredtasks.Where(t => t.Responsible== RespFilter).ToList();
                if (DoneFilter != "All") l_filteredtasks = l_filteredtasks.Where(t => t.Done == (DoneFilter=="Done")).ToList();
                

                if(SortAscending)l_filteredtasks = l_filteredtasks.OrderBy(t => t.GetType().GetProperty(Sorter).GetValue(t)).ToList();
                else l_filteredtasks = l_filteredtasks.OrderByDescending(t => t.GetType().GetProperty(Sorter).GetValue(t)).ToList();

                var filteredtasks = new ObservableCollection<Task_>(l_filteredtasks);
                return filteredtasks ;
            }
        }

        private string _namefilter;
        public string NameFilter
        {
            get
            {
                return _namefilter;
            }
            set
            {
                _namefilter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();


            }
        }

        private string _donefilter ="All";
        public string DoneFilter
        {
            get
            {
                return _donefilter;
            }
            set
            {
                _donefilter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        public List<string> PossibleDoneFilter
        {
            get
            {
                return new List<string> {"All", "Done", "Undone" };
            }
        }

        private string _respfilter = "All";
        public string RespFilter
        {
            get
            {
                return _respfilter;
            }
            set
            {
                _respfilter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        public List<string> PossibleRespFilter
        {
            get
            {
                List<string> filter = Tasks.Select(t => t.Responsible).Distinct().ToList();
                filter.Insert(0,"All");
                return filter;
            }
        }

        private string _sorter = "Delay";
        public string Sorter
        {
            get
            {
                return _sorter;
            }
            set
            {
                _sorter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        public List<string> PossibleSorter
        {
            get
            {
                return typeof(Task_).GetProperties().Select(p=>p.Name).OrderBy(n=>n).ToList();
            }
        }

        private bool _sortAscending= true;
        public bool SortAscending
        {
            get
            {
                return _sortAscending;
            }
            set
            {
                _sortAscending = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }


        #endregion Properties

        #region Commands

        private ICommand _addCommand;
        private bool CanAdd = true;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(
                        p => this.CanAdd,
                        p => this.AddTask());
                }
                return _addCommand;
            }
        }

        private ICommand _editCommand;
        private bool CanEdit;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        p => this._selectedtask != null,
                        p => this.EditTask());
                }
                return _editCommand;
            }
        }

        private ICommand _deleteCommand;
        private bool CanDelete = true;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        p => this._selectedtask != null,
                        p => this.DeleteTask());
                }
                return _deleteCommand;
            }
        }

        private ICommand _saveandquitCommand;
        private bool CanSaveAndQuit = true;
        public ICommand SaveAndQuitCommand
        {
            get
            {
                if (_saveandquitCommand == null)
                {
                    _saveandquitCommand = new RelayCommand(
                        p => true,
                        p => this.SaveAndQuit());
                }
                return _saveandquitCommand;
            }
        }

        #endregion Commands

        #region Methods
        /// <summary>
        /// To call a empty taskview
        /// </summary>
        private void AddTask()
        {
            int id = _tasks.Select(t => t.ID).Max() + 1;
            var tsk = new Task_()
            {
                ID = id,
                Delay = DateTime.Now
            };
            mainWindowViewModel.DisplayTaskView(tsk);
        }

        /// <summary>
        /// To call a taskview filled with selected task
        /// </summary>
        private void EditTask()
        {
            mainWindowViewModel.DisplayTaskView(_selectedtask);
        }

        /// <summary>
        /// to delete selectedtask from Tasks
        /// </summary>
        private void DeleteTask()
        {
            var oldtasklist = Tasks;
            oldtasklist.Remove(_selectedtask);
            Tasks = oldtasklist;
        }

        public async void SaveAndQuit()
        {
            await IO.SaveTasksAsync(Tasks, filename);
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// To save a task in Tasks : replace old or make new task
        /// </summary>
        /// <param name="task"></param>
        internal void SaveTask(Task_ task)
        {
            if (task.ID == null) return;

            if (Tasks.Select(t => t.ID).Contains(task.ID))
            {
                Tasks[Tasks.IndexOf(Tasks.Where(t => t.ID == task.ID).FirstOrDefault())] = task;
            }
            else
            {
                Tasks.Add(task);
            }
            SelectedTask = task;
        }

        #endregion Methods

        #region Tasks
        public Task Initialize { get; }
        /// <summary>
        /// To import data from file
        /// </summary>
        /// <returns></returns>
        private async Task ImportDataAsync()
        {
            _tasks = await IO.GetTasksAsync(filename);
        }
        #endregion Tasks


    }


}
