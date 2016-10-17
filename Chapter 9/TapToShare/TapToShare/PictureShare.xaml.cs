using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TapToShare.ViewModels;
using System.Windows.Input;

namespace TapToShare
{
    public partial class PictureShare : PhoneApplicationPage
    {
        private PictureShareViewModel shareViewModel;

        public PictureShare()
        {
            InitializeComponent();

            shareViewModel = new PictureShareViewModel();
            this.DataContext = shareViewModel;            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            shareViewModel.StartAdvertising();
        }
                
        private void SendPicture_Tap(object sender, GestureEventArgs e)
        {
            shareViewModel.StartRandomPictureShare();
        }

        private void StopSharing_Tap(object sender, GestureEventArgs e)
        {
            shareViewModel.CloseConnection();
            shareViewModel.StartAdvertising();
        }
    }
}