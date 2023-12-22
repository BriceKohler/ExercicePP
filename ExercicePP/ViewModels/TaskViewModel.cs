namespace ExercicePP.ViewModels
{
    using System.Windows.Input;
    using ExercicePP.Models;

    /// <summary>
    /// Viewmodel of the task view.
    /// </summary>
    internal class TaskViewModel : ViewModelBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskViewModel"/> class.
        /// </summary>
        /// <param name="task">task.</param>
        /// <param name="mainwindowViewModel">MainWindowViewModel.</param>
        public TaskViewModel(Task_ task, MainWindowViewModel mainwindowViewModel)
        {
            this.task = task.Copy();
            this.mainwindowViewModel = mainwindowViewModel;
        }

        #endregion Constructor

        #region Variables

        /// <summary>
        /// MainwindowViewModel to throw back after this view.
        /// </summary>
        private readonly MainWindowViewModel mainwindowViewModel;

        #endregion Variables

        #region Properties

        /// <summary>
        /// Task to show.
        /// </summary>
        private Task_ task;

        /// <summary>
        /// Gets or sets task to show.
        /// </summary>
        public Task_ Task
        {
            get
            {
                return this.task;
            }

            set
            {
                this.task = value;
                this.OnPropertyChanged();
            }
        }
        #endregion Properties

        #region Commands

        /// <summary>
        /// Command to call task's saving.
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// Boolean to define whether saving is authorized or not.
        /// </summary>
        private readonly bool canSave = true;

        /// <summary>
        /// Gets command to call task's saving.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(
                        p => this.canSave,
                        p => this.SaveTask());
                }
                return this.saveCommand;
            }
        }

        /// <summary>
        /// Command to call edition canceling.
        /// </summary>
        private ICommand cancelCommand;

        /// <summary>
        /// Boolean to define whether canceling is authorized or not.
        /// </summary>
        private readonly bool canCancel = true;

        /// <summary>
        /// Gets Command to call edition canceling.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if (this.cancelCommand == null)
                {
                    this.cancelCommand = new RelayCommand(
                        p => this.canCancel,
                        p => this.CancelTask());
                }
                return this.cancelCommand;
            }
        }

        #endregion Commands

        #region Methods

        /// <summary>
        /// To save the new or existing task.
        /// </summary>
        public void SaveTask()
        {
            this.mainwindowViewModel.SaveTask(this.Task);
        }

        /// <summary>
        /// To cancel task editing.
        /// </summary>
        public void CancelTask()
        {
            this.mainwindowViewModel.CancelSave();
        }

        #endregion Methods


    }
}
