using Microsoft.Phone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace MyCameraApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        CameraCaptureTask camera;

        public MainPage()
        {
            InitializeComponent();
            camera = new CameraCaptureTask();
            camera.Completed += camera_Completed;
        }
        
        private void camera_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK && e.Error == null)
            {
                System.Windows.Media.Imaging.BitmapImage image = 
                    new System.Windows.Media.Imaging.BitmapImage();

                image.SetSource(e.ChosenPhoto); 

                myPicture.Source = image;

                statusMessage.Text = "Photo captured using the CameraCaptureTask. Photo has been saved to the Camera Roll.";
            }
        }

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            camera.Show();
        }

    }
}