﻿#pragma checksum "C:\Users\Lori\Documents\Apress\9781430259022_Lalonde_Ch14_Publish\Source\MyWindowsPhoneApp\MyWindowsPhoneApp\MyPanoramaPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "00E4515973AF67A532ABC8791605B8BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Advertising.Mobile.UI;
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


namespace MyWindowsPhoneApp {
    
    
    public partial class MyPanoramaPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Advertising.Mobile.UI.AdControl myAdControl;
        
        internal Microsoft.Advertising.Mobile.UI.AdControl myAdControl2;
        
        internal Microsoft.Advertising.Mobile.UI.AdControl myAdControl3;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyWindowsPhoneApp;component/MyPanoramaPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.myAdControl = ((Microsoft.Advertising.Mobile.UI.AdControl)(this.FindName("myAdControl")));
            this.myAdControl2 = ((Microsoft.Advertising.Mobile.UI.AdControl)(this.FindName("myAdControl2")));
            this.myAdControl3 = ((Microsoft.Advertising.Mobile.UI.AdControl)(this.FindName("myAdControl3")));
        }
    }
}

