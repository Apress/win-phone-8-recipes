using Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ApplicationBarButtonMenuItem.ViewModels
{

    internal class MainPageViewModel : ViewModel
    {
        // we'll use this to flip the visibility 
        // one per second for demo purposes
        private DispatcherTimer timer;

        public MainPageViewModel()
        {
            //timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 1); // five seconds
            //timer.Tick += (s, e) =>
            //{
            //    IsApplicationBarVisible = !IsApplicationBarVisible;
            //};

            //timer.Start();

            AdvancedOptionsCommand = new RelayCommand(AdvancedOptionsCommandExecute, AdvancedOptionsCommandCanExecute);

        }
        
        private bool isApplicationBarVisible = true;
        public bool IsApplicationBarVisible
        {
            get
            {
                return isApplicationBarVisible;
            }
            set
            {
                isApplicationBarVisible = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand advancedOptionsCommand;
        public RelayCommand AdvancedOptionsCommand
        {
            get
            {
                return advancedOptionsCommand;
            }
            set
            {
                advancedOptionsCommand = value;
                RaisePropertyChanged();
            }
        }

        private bool AdvancedOptionsCommandCanExecute(object parameter)
        {
            return true;

        }

        private void AdvancedOptionsCommandExecute(object parameter)
        {
            MessageBox.Show("Advanced Options selected.");

        }
    }
}
