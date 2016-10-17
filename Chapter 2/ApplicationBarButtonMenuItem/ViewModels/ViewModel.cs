using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Common.Library
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
