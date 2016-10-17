using System.Linq;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Imaging;

namespace MyAdvancedCameraApp
{
    public partial class PhotoDetailsPage : PhoneApplicationPage
    {
       
        public PhotoDetailsPage()
        {
            InitializeComponent();
        }

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    if (NavigationContext.QueryString.ContainsKey("pic"))
    {
        string imageName = NavigationContext.QueryString["pic"];

        using (MediaLibrary myLibrary = new MediaLibrary())
        {
            Picture myPic = myLibrary.SavedPictures.Where(p => p.Name == imageName).FirstOrDefault();
            if (myPic != null)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(myPic.GetImage());
                myImage.Source = bmp;

                imageDetails.Text = string.Format("Image saved as {0} in album {1}", imageName, myPic.Album.Name);
            }
        }
    }
}
    }
}