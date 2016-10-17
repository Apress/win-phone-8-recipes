using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyEmergencyInfo.ViewModels;
using System.Windows.Input;

namespace MyEmergencyInfo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.ViewModel.LoadInfo();
        }


        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            UpdateTextBoxBinding();
            if (App.ViewModel.SaveInfo())
            {
                MessageBox.Show("Your emergency info has been encrypted and saved to a local file!");
            }
            else
            {
                MessageBox.Show("Something went wrong in attempting to save your info. Try again later.");
            }
        }

        private void UpdateTextBoxBinding()
        {
            //force an update of the binding for the textbox that has focus 
            //so the current text is reflected in the viewmodel property
            object focusedElement = FocusManager.GetFocusedElement();
            if (focusedElement != null && focusedElement is TextBox)
            {
                TextBox textBox = (TextBox)focusedElement;
                var binding = textBox.GetBindingExpression(TextBox.TextProperty);
                binding.UpdateSource();
            }

        }

    }
}