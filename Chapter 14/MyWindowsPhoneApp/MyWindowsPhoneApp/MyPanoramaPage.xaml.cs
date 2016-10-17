using Microsoft.Phone.Controls;
using Microsoft.Advertising.Mobile.UI;

namespace MyWindowsPhoneApp
{
    public partial class MyPanoramaPage : PhoneApplicationPage
    {
        private string applicationId = "test_client";
        private string adUnitId1 = "Image480_80";
        private string adUnitId2 = "Image300_50";
        private string adUnitId3 = "TextAd";
        
        public MyPanoramaPage()
        {
            InitializeComponent();
        }

        private void AdControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            myAdControl = new AdControl(applicationId, adUnitId1, true);
        }

        private void AdControl2_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            myAdControl2 = new AdControl(applicationId, adUnitId2, true);
        }

        private void AdControl3_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            myAdControl3 = new AdControl(applicationId, adUnitId3, true);
        }    
    }
}