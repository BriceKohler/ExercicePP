using ExercicePP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExercicePP.ViewModels
{
    class TaskViewModel : ViewModelBase
    {
        #region Constructor

        public TaskViewModel(Task_ task, MainWindowViewModel mainwindowViewModel)
        {
            _task = task.Copy();
            _mainwindowViewModel = mainwindowViewModel;
        }
        #endregion Constructor

        #region Variables

        private Task_ oldtask;
        private MainWindowViewModel _mainwindowViewModel;
        #endregion Variables

        #region Properties
        private Task_ _task;
        public Task_ Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties

        #region Commands


        private ICommand _saveCommand;
        private bool CanSave = true;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        p => this.CanSave,
                        p => this.SaveTask());
                }
                return _saveCommand;
            }
        }

        private ICommand _cancelCommand;
        private bool CanCancel = true;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        p => this.CanCancel,
                        p => this.CancelTask());
                }
                return _cancelCommand;
            }
        }

        #endregion Commands

        #region Methods
        /// <summary>
        /// To save the new or existing task
        /// </summary>
        public void SaveTask()
        {
            _mainwindowViewModel.SaveTask(Task);
        }
        /// <summary>
        /// to cancel task editing
        /// </summary>
        public void CancelTask()
        {
            _mainwindowViewModel.CancelSave();
        }

        #endregion Methods


    }
}
