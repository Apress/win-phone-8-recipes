using Microsoft.Phone.Controls;
using Microsoft.Advertising.Mobile.UI;

namespace MyWindowsPhoneApp
{
    public partial class MyPivotPage : PhoneApplicationPage
    {
        private string applicationId = "test_client";
        private string adUnitId = "Imag480_80";
        
        public MyPivotPage()
        {
            InitializeComponent();
        }

        private void AdControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            myAdControl = new AdControl(applicationId, adUnitId, true);
        }
    }
}