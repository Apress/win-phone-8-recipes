﻿#pragma checksum "C:\Users\Lori\Documents\Apress\chapter 13\9781430259022_Lalonde_Ch13_Source\MyLiveConnectAppWithCalendar\MyLiveConnectApp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ADE77648AAFE75A3D1561E236901333D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Live.Controls;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MyLiveConnectApp {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Live.Controls.SignInButton signInButton;
        
        internal System.Windows.Controls.TextBlock signInStatus;
        
        internal System.Windows.Controls.StackPanel HubPanelTop;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLiveConnectApp;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.signInButton = ((Microsoft.Live.Controls.SignInButton)(this.FindName("signInButton")));
            this.signInStatus = ((System.Windows.Controls.TextBlock)(this.FindName("signInStatus")));
            this.HubPanelTop = ((System.Windows.Controls.StackPanel)(this.FindName("HubPanelTop")));
        }
    }
}

