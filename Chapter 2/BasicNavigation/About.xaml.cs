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
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        protected override void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            string frag = e.Fragment;

            switch (frag)
            {
                case "privacy":
                    
                    break;
                case "contact":
                    aboutPivot.SelectedIndex = 1;
                    break;
            
            }
        }
    }
}