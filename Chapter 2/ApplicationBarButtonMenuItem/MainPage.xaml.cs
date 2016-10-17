using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ApplicationBarButtonMenuItem.ViewModels;
using System.Windows.Data;

namespace ApplicationBarButtonMenuItem
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // uncomment the next line if you would rather
            // configure the ApplicationBar in code.  You will
            // also need to remove the ApplicaitonBar XAML from
            // MainPage.xaml
            //BuildApplicationBar();
            
            Model = new MainPageViewModel();
            
            Binding menuEnabledBinding = new Binding("IsApplicationBarVisible");
            SetBinding(IsApplicationBarVisibleProperty, menuEnabledBinding);

        }

        private MainPageViewModel Model
        {
            get
            {
                return (MainPageViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public static readonly DependencyProperty IsApplicationBarVisibleProperty = DependencyProperty.Register("IsApplicationBarVisible", typeof(bool), typeof(MainPage), new PropertyMetadata(true, OnIsApplicationBarEnabledChanged));

        private static void OnIsApplicationBarEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MainPage)(d)).OnIsApplicationBarVisibleChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private void OnIsApplicationBarVisibleChanged(bool oldValue, bool newValue)
        {
            ApplicationBar.IsVisible = newValue;
        }

        public bool IsApplicationBarMenuEnabled
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (bool)GetValue(IsApplicationBarVisibleProperty);
            }
            set
            {
                SetValue(IsApplicationBarVisibleProperty, value);
            }
        }


        private void BuildApplicationBar()
        {
            //ApplicationBar = new ApplicationBar();

            //ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/like.png", UriKind.Relative));

            //appBarButton.Text = "like";
            //ApplicationBar.Buttons.Add(appBarButton);

            //appBarButton = new ApplicationBarIconButton(new Uri("/Assets/save.png", UriKind.Relative));
            //appBarButton.Text = "save";
            //ApplicationBar.Buttons.Add(appBarButton);

            //// Create a new menu item with the localized string from AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem("advanced options");
            //appBarMenuItem.Click += AdvancedOptionsClick;
            //ApplicationBar.MenuItems.Add(appBarMenuItem);

        }

        void AdvancedOptionsClick(object sender, EventArgs e)
        {
            if (Model.AdvancedOptionsCommand.CanExecute(null))
            {
                Model.AdvancedOptionsCommand.Execute(null);
            }
            
        }
    }
}