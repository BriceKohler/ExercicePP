// <copyright file="MainWindowViewModel.cs" company="Kohler, Brice">
// Copyright (c) Kohler, Brice. All rights reserved.
// </copyright>

namespace ExercicePP.ViewModels
{
    using ExercicePP.Models;

    /// <summary>
    /// Viewmodel of the main window.
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Variables

        /// <summary>
        /// ToDo list Viewmodel.
        /// </summary>
        private readonly ToDoListViewModel toDoListViewModel;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.toDoListViewModel = new ToDoListViewModel(this);
            this.selectedViewModel = this.toDoListViewModel;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// Selected Viewmodel to show.
        /// </summary>
        private ViewModelBase selectedViewModel;

        /// <summary>
        /// Gets or sets selected Viewmodel to show.
        /// </summary>
        public ViewModelBase SelectedViewModel
        {
            get
            {
                return this.selectedViewModel;
            }

            set
            {
                this.selectedViewModel = value;
                this.OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// switching from ToDoListView to TaskView.
        /// </summary>
        /// <param name="task">Task to show.</param>
        public void DisplayTaskView(Task_ task)
        {
            this.SelectedViewModel = new TaskViewModel(task, this);
        }

        /// <summary>
        /// saving Task and switching from TaskView to ToDoListView 
        /// </summary>
        /// <param name="task">task to save.</param>
        public void SaveTask(Task_ task)
        {
            this.toDoListViewModel.SaveTask(task);
            this.SelectedViewModel = this.toDoListViewModel;
        }

        /// <summary>
        /// Only switching from TaskView to ToDoListView.
        /// </summary>
        public void CancelSave()
        {
            this.SelectedViewModel = this.toDoListViewModel;
        }

        #endregion Methods
    }
}
