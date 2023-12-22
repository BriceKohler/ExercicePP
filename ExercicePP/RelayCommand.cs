// <copyright file="RelayCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExercicePP
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Class to implement RelayCommand.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="canExecute">Indicates if command can execute.</param>
        /// <param name="execute">Action to exectute.</param>
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        /// <summary>
        /// Handle when action can be executed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Casts can execute object.
        /// </summary>
        /// <param name="parameter">canexecute object</param>
        /// <returns>canexecute boolean.</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        /// <summary>
        /// Executes action.
        /// </summary>
        /// <param name="parameter">Action.</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
