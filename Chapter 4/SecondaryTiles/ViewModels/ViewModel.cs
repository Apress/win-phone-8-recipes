using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SecondaryTiles.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// By declaring our event and initalizing it with an empty delegate we
        /// can avoid the messy business of making a copy and checking for null
        /// when raising the event.  By definition, it can never be null.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate
        {
        };

        /// <summary>
        /// Notify subscribers of the event that a change has occurred.
        /// </summary>
        /// <remarks>
        /// Note the use of the <see cref="System.Runtime.CompilerServices.CallerMemberNameAttribute"/> 
        /// to automatically populate the <paramref name="propertyName"/> parameter
        /// </remarks>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
