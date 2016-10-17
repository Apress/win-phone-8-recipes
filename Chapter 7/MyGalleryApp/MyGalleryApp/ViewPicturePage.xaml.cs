using Microsoft.Phone.Controls;

namespace MyGalleryApp
{
    public partial class ViewPicturePage : PhoneApplicationPage
    {
        public ViewPicturePage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }
    }
}