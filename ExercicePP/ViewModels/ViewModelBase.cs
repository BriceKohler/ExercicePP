using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExercicePP.ViewModels
{
    /// <summary>
    /// To implement INotifiyPropertyChanged in every view
    /// </summary>
    class ViewModelBase:INotifyPropertyChanged
    {
        #region Notifications

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Notifications

        #region OnPropertyChanged
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion OnPropertyChanged
    }
}
