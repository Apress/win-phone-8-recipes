﻿#pragma checksum "C:\Users\Lori\Documents\Apress\Chapter 3\TapIt\TapIt\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "02AE31ECF4E136F2AF3DDBF6E8BD6DF5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace TapIt {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.WrapPanel ContentPanel;
        
        internal System.Windows.Controls.Primitives.Popup menuEllipse;
        
        internal System.Windows.Controls.ListBox listMenuItems;
        
        internal System.Windows.Controls.ListBoxItem itemColorChange;
        
        internal System.Windows.Controls.ListBoxItem itemNew;
        
        internal System.Windows.Controls.ListBoxItem itemDelete;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/PhoneApp1;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((Microsoft.Phone.Controls.WrapPanel)(this.FindName("ContentPanel")));
            this.menuEllipse = ((System.Windows.Controls.Primitives.Popup)(this.FindName("menuEllipse")));
            this.listMenuItems = ((System.Windows.Controls.ListBox)(this.FindName("listMenuItems")));
            this.itemColorChange = ((System.Windows.Controls.ListBoxItem)(this.FindName("itemColorChange")));
            this.itemNew = ((System.Windows.Controls.ListBoxItem)(this.FindName("itemNew")));
            this.itemDelete = ((System.Windows.Controls.ListBoxItem)(this.FindName("itemDelete")));
        }
    }
}

