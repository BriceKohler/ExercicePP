namespace ExercicePP.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ExercicePP.Models;
    using ExercicePP.Utilities;

    /// <summary>
    /// Viewmodel of the todo list view.
    /// </summary>
    internal class ToDoListViewModel : ViewModelBase
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoListViewModel"/> class.
        /// </summary>
        /// <param name="mainwindow">Main window.</param>
        public ToDoListViewModel(MainWindowViewModel mainwindow)
        {
            this.Initialize = this.ImportDataAsync();
            this.mainWindowViewModel = mainwindow;
        }
        #endregion Constructor

        #region Variables

        /// <summary>
        /// Parent MainWindowViewModel.
        /// </summary>
        private readonly MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// Saving filename.
        /// </summary>
        private readonly string filename = @".\Tasks.txt";

        #endregion Variables

        #region Properties

        /// <summary>
        /// Collection of tasks.
        /// </summary>
        private ObservableCollection<Task_> tasks;

        /// <summary>
        /// Gets or sets collection of tasks.
        /// </summary>
        public ObservableCollection<Task_> Tasks
        {
            get
            {
                return this.tasks;
            }

            set
            {
                this.tasks = value;
                this.OnPropertyChanged("FilteredTasks");
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Selected task.
        /// </summary>
        private Task_ selectedtask;

        /// <summary>
        /// Gets or sets selected task.
        /// </summary>
        public Task_ SelectedTask
        {
            get
            {
                return this.selectedtask;
            }

            set
            {
                this.selectedtask = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Get filtered tasks.
        /// </summary>
        public ObservableCollection<Task_> FilteredTasks
        {
            get
            {
                var l_filteredtasks = Tasks.ToList();
                if (NameFilter != null) l_filteredtasks = l_filteredtasks.Where(t => t.TaskName.Contains(NameFilter)).ToList();
                if (RespFilter != "All") l_filteredtasks = l_filteredtasks.Where(t => t.Responsible== RespFilter).ToList();
                if (DoneFilter != "All") l_filteredtasks = l_filteredtasks.Where(t => t.Done == (DoneFilter=="Done")).ToList();

                if (SortAscending)l_filteredtasks = l_filteredtasks.OrderBy(t => t.GetType().GetProperty(Sorter).GetValue(t)).ToList();
                else l_filteredtasks = l_filteredtasks.OrderByDescending(t => t.GetType().GetProperty(Sorter).GetValue(t)).ToList();

                var filteredtasks = new ObservableCollection<Task_>(l_filteredtasks);
                return filteredtasks ;
            }
        }

        /// <summary>
        /// Filter for task name.
        /// </summary>
        private string namefilter;

        /// <summary>
        /// Gets or sets filter for task name.
        /// </summary>
        public string NameFilter
        {
            get
            {
                return namefilter;
            }

            set
            {
                namefilter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Filter for done status.
        /// </summary>
        private string donefilter = "All";

        /// <summary>
        /// Gets or sets filter for done status.
        /// </summary>
        public string DoneFilter
        {
            get
            {
                return donefilter;
            }

            set
            {
                donefilter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets possible done status filters.
        /// </summary>
        public List<string> PossibleDoneFilter
        {
            get
            {
                return new List<string> {"All", "Done", "Undone" };
            }
        }

        /// <summary>
        /// Filter for responsible.
        /// </summary>
        private string respfilter = "All";

        /// <summary>
        /// Gets or sets filter for responsible;
        /// </summary>
        public string RespFilter
        {
            get
            {
                return respfilter;
            }

            set
            {
                respfilter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets possible responsible filters.
        /// </summary>
        public List<string> PossibleRespFilter
        {
            get
            {
                List<string> filter = Tasks.Select(t => t.Responsible).Distinct().ToList();
                filter.Insert(0,"All");
                return filter;
            }
        }

        /// <summary>
        /// Task's sorter.
        /// </summary>
        private string sorter = "Delay";

        /// <summary>
        /// Gets or sets task's sorter.
        /// </summary>
        public string Sorter
        {
            get
            {
                return sorter;
            }

            set
            {
                sorter = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets possible task's sorter.
        /// </summary>
        public List<string> PossibleSorter
        {
            get
            {
                return typeof(Task_).GetProperties().Select(p=>p.Name).OrderBy(n=>n).ToList();
            }
        }

        /// <summary>
        /// Task sorter parameter.
        /// </summary>
        private bool sortAscending= true;

        /// <summary>
        /// Gets or sets a value indicating whether sort ascending or descending.
        /// </summary>
        public bool SortAscending
        {
            get
            {
                return sortAscending;
            }

            set
            {
                sortAscending = value;
                OnPropertyChanged("FilteredTasks");
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Commands

        /// <summary>
        /// Command to add task.
        /// </summary>
        private ICommand addCommand;

        /// <summary>
        /// Boolean to define whether adding is authorized or not.
        /// </summary>
        private bool canAdd = true;

        /// <summary>
        /// Gets command to add task.
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(
                        p => this.canAdd,
                        p => this.AddTask());
                }

                return addCommand;
            }
        }

        /// <summary>
        /// Command to edit task.
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// Gets command to edit task.
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(
                        p => this.selectedtask != null,
                        p => this.EditTask());
                }

                return editCommand;
            }
        }

        /// <summary>
        /// Command to delete task.
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// Gets command to delete task.
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(
                        p => this.selectedtask != null,
                        p => this.DeleteTask());
                }
                return deleteCommand;
            }
        }

        /// <summary>
        /// Command to save tasks and quit.
        /// </summary>
        private ICommand saveandquitCommand;

        /// <summary>
        /// Boolean to define whether saving and quit is authorized or not.
        /// </summary>
        private bool canSaveAndQuit = true;

        /// <summary>
        /// Gets command to save and quit.
        /// </summary>
        public ICommand SaveAndQuitCommand
        {
            get
            {
                if (saveandquitCommand == null)
                {
                    saveandquitCommand = new RelayCommand(
                        p => canSaveAndQuit,
                        p => this.SaveAndQuit());
                }

                return saveandquitCommand;
            }
        }

        #endregion Commands

        #region Methods

        /// <summary>
        /// To call a empty taskview.
        /// </summary>
        private void AddTask()
        {
            int id = tasks.Select(t => t.ID).Max() + 1;
            var tsk = new Task_()
            {
                ID = id,
                Delay = DateTime.Now,
            };
            mainWindowViewModel.DisplayTaskView(tsk);
        }

        /// <summary>
        /// To call a taskview filled with selected task.
        /// </summary>
        private void EditTask()
        {
            mainWindowViewModel.DisplayTaskView(selectedtask);
        }

        /// <summary>
        /// to delete selectedtask from Tasks.
        /// </summary>
        private void DeleteTask()
        {
            var oldtasklist = Tasks;
            oldtasklist.Remove(selectedtask);
            Tasks = oldtasklist;
        }

        /// <summary>
        /// To save tasklist and quit.
        /// </summary>
        public async void SaveAndQuit()
        {
            await IO.SaveTasksAsync(Tasks, filename);
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// To save a task in Tasks : replace old or make new task.
        /// </summary>
        /// <param name="task">task to add.</param>
        internal void SaveTask(Task_ task)
        {
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

        /// <summary>
        /// Gets task to initialize tasklist.
        /// </summary>
        public Task Initialize { get; }

        /// <summary>
        /// To import data from file.
        /// </summary>
        /// <returns>nothing</returns>
        private async Task ImportDataAsync()
        {
            tasks = await IO.GetTasksAsync(filename);
        }
        #endregion Tasks


    }


}
