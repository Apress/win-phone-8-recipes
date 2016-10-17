using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PinchIt.Resources;
using System.Windows.Media;

namespace PinchIt
{
    public partial class MainPage : PhoneApplicationPage
    {
        private double imageScale = 1;

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            GestureListener listener = GestureService.GetGestureListener(myPhoto);
            listener.PinchStarted += new EventHandler<PinchStartedGestureEventArgs>(GestureListener_PinchStarted);
            listener.PinchDelta += new EventHandler<PinchGestureEventArgs>(GestureListener_PinchDelta);
            listener.PinchCompleted += new EventHandler<PinchGestureEventArgs>(GestureListener_PinchCompleted);

            photoScaleText.Text = string.Format("Size: {0}X{1}", myPhoto.Width, myPhoto.Height);
        }

        void GestureListener_PinchStarted(object sender, PinchStartedGestureEventArgs e)
        {
            //get the current photo's scale value
            imageScale = photoTransform.ScaleX;
            
            //get the two touch points
            Point touchPointA = e.GetPosition(myPhoto, 0);
            Point touchPointB = e.GetPosition(myPhoto, 1);

            //set the photo center x and y axis to the center of the 
            //corresponding axis between the two touch points
            photoTransform.CenterX = touchPointA.X + (touchPointB.X - touchPointA.X) / 2;
            photoTransform.CenterY = touchPointA.Y + (touchPointB.Y - touchPointA.Y) / 2;
        }

        void GestureListener_PinchDelta(object sender, PinchGestureEventArgs e)
        {
           
            //scale the photo as the pinch gesture changes
            photoTransform.ScaleX = imageScale * e.DistanceRatio;
            photoTransform.ScaleY = photoTransform.ScaleX;
            photoScaleText.Text = string.Format("Size: {0}X{1}, Scale:{2} - Pinch In Progress",
                myPhoto.Width, myPhoto.Height, Math.Round(photoTransform.ScaleX, 2));       

        }

        void GestureListener_PinchCompleted(object sender, PinchGestureEventArgs e)
        {
            photoScaleText.Text = string.Format("Size: {0}X{1}, Scale:{2} - Pinch Completed",
                myPhoto.Width, myPhoto.Height, Math.Round(photoTransform.ScaleX, 2));
        }
        
    }
}