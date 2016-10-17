using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace BasicNavigation
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            instructionTextBlock.Text = "Please check below to indicate you have seen this screen.";
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (!confirmationCheckBox.IsChecked ?? false)
            {
                if (e.IsCancelable)
                {
                    MessageBox.Show("You must check the confirmation check box to proceed");
                    e.Cancel = true;
                }
            }
        }
    }
}