﻿#pragma checksum "D:\Apress\APress\Chapter 11\Source\MyBookListEncrypted\MyBookListEncrypted\DetailsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B9199945DE749826AA1F93800DC784B"
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


namespace MyBookList {
    
    
    public partial class DetailsPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.Pivot bookInfoPivot;
        
        internal Microsoft.Phone.Controls.PivotItem detailsPivot;
        
        internal System.Windows.Controls.TextBox titleText;
        
        internal System.Windows.Controls.TextBox authorText;
        
        internal Microsoft.Phone.Controls.PivotItem notePivot;
        
        internal Microsoft.Phone.Controls.LongListSelector NotesLongListSelector;
        
        internal System.Windows.Controls.Primitives.Popup popupNote;
        
        internal System.Windows.Controls.TextBox noteText;
        
        internal System.Windows.Controls.Button addButton;
        
        internal System.Windows.Controls.Button cancelButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyBookList;component/DetailsPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.bookInfoPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("bookInfoPivot")));
            this.detailsPivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("detailsPivot")));
            this.titleText = ((System.Windows.Controls.TextBox)(this.FindName("titleText")));
            this.authorText = ((System.Windows.Controls.TextBox)(this.FindName("authorText")));
            this.notePivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("notePivot")));
            this.NotesLongListSelector = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("NotesLongListSelector")));
            this.popupNote = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupNote")));
            this.noteText = ((System.Windows.Controls.TextBox)(this.FindName("noteText")));
            this.addButton = ((System.Windows.Controls.Button)(this.FindName("addButton")));
            this.cancelButton = ((System.Windows.Controls.Button)(this.FindName("cancelButton")));
        }
    }
}

