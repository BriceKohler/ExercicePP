// <copyright file="ViewModelBase.cs" company="Kohler, Brice">
// Copyright (c) Kohler, Brice. All rights reserved.
// </copyright>

namespace ExercicePP.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// To implement INotifiyPropertyChanged in every view.
    /// </summary>
    internal class ViewModelBase : INotifyPropertyChanged
    {
        #region Notifications

        /// <summary>
        /// Handles the property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Notifications

        #region OnPropertyChanged

        /// <summary>
        /// Indicates the propoerty has changed.
        /// </summary>
        /// <param name="name">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion OnPropertyChanged
    }
}
