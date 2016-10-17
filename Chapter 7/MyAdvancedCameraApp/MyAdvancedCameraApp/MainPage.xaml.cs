using System;
using System.Windows.Navigation;
using Microsoft.Devices;
using Microsoft.Phone.Controls;

namespace MyAdvancedCameraApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCamera myCamera;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhotoCamera.IsCameraTypeSupported(CameraType.Primary))
            {
                CameraButtons.ShutterKeyHalfPressed += CameraButtons_ShutterKeyHalfPressed;
                CameraButtons.ShutterKeyPressed += CameraButtons_ShutterKeyPressed;
                CameraButtons.ShutterKeyReleased += CameraButtons_ShutterKeyReleased;

                myCamera = new PhotoCamera(CameraType.Primary);               
                myCamera.AutoFocusCompleted += myCamera_AutoFocusCompleted;
                myCamera.CaptureImageAvailable += myCamera_CaptureImageAvailable;

                this.Tap += MainPage_Tap;
                viewfinderBrush.SetSource(myCamera);

                cameraState.Text = "Tap the screen to focus and take a picture or press the hardware camera button to take a picture";

            }
            else
            {
                cameraState.Text = "Camera not supported";
            }
        }

        void MainPage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (myCamera != null)
            {
                myCamera.Focus();
                SetCameraStateMessage("Camera focus initiated through screen tap");
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CameraButtons.ShutterKeyHalfPressed -= CameraButtons_ShutterKeyHalfPressed;
            CameraButtons.ShutterKeyPressed -= CameraButtons_ShutterKeyPressed;
            CameraButtons.ShutterKeyReleased -= CameraButtons_ShutterKeyReleased;

            myCamera.AutoFocusCompleted -= myCamera_AutoFocusCompleted;
            myCamera.CaptureImageAvailable -= myCamera_CaptureImageAvailable;

            myCamera.Dispose();

        }

        private void myCamera_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            string imageName = string.Format("MyAdvCam{0}.jpg", DateTime.Now.Ticks.ToString());

            using (Microsoft.Xna.Framework.Media.MediaLibrary library = new Microsoft.Xna.Framework.Media.MediaLibrary())
            {
                //save picture to device's Saved Pictures album
                library.SavePicture(imageName, e.ImageStream);
            };

            this.Dispatcher.BeginInvoke(() =>
                {
                    NavigationService.Navigate(new Uri("/PhotoDetailsPage.xaml?pic=" + imageName, UriKind.Relative));
                });
        }

        private void myCamera_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            if (myCamera != null)
            {
                myCamera.CaptureImage();
                SetCameraStateMessage("Auto focus completed, camera capture initiated");
            }
        }
                
        private void CameraButtons_ShutterKeyReleased(object sender, EventArgs e)
        {
            if (myCamera != null)
            {
                myCamera.CancelFocus();
                SetCameraStateMessage("Camera focus cancelled");
            }
        }

        private void CameraButtons_ShutterKeyPressed(object sender, EventArgs e)
        {
            if (myCamera != null)
            {
                myCamera.CaptureImage();
                SetCameraStateMessage("Camera capture initiated");
            }
        }

        private void CameraButtons_ShutterKeyHalfPressed(object sender, EventArgs e)
        {
            if (myCamera != null)
            {
                myCamera.Focus();
                SetCameraStateMessage("Camera focus initiated");
            }
        }

        private void SetCameraStateMessage(string message)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                cameraState.Text = message;
            });
        }
        

    }
}