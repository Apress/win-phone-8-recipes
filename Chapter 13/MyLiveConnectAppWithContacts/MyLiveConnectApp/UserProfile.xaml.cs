﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MyLiveConnectApp
{
    public partial class UserProfile : PhoneApplicationPage
    {
        public UserProfile()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel.User;
        }
    }
}