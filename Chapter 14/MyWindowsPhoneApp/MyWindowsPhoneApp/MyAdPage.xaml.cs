using Microsoft.Phone.Controls;
using Microsoft.Advertising.Mobile.UI;

namespace MyWindowsPhoneApp
{
    public partial class MyAdPage : PhoneApplicationPage
    {
        private string applicationId = "test_client";
        private string adUnitId = "TextAd";
                
        public MyAdPage()
        {
            InitializeComponent();
        }

        private void AdControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            ErrorText.Text = e.Error.Message;
            if (e.Error.InnerException != null)
                ErrorText.Text += System.Environment.NewLine + e.Error.InnerException.Message;

            myAdControl = new AdControl(applicationId, adUnitId, true);
        }        
    }
}