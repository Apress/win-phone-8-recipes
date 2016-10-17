using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyFirstWPApp.Resources;

namespace MyFirstWPApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }


        private void greetMeButton_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (nameText.Text.Length == 0)
            {
                greetingText.Text = "Please enter your name to receive a proper greeting";
            }
            else
            {
                greetingText.Text = String.Format("It's a pleasure to meet you, {0}", nameText.Text);
            }
        }


    }
}