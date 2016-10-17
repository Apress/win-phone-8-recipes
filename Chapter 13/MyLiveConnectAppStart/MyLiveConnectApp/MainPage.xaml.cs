using System.Windows;
using Microsoft.Phone.Controls;

namespace MyLiveConnectApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }
        
    }
}