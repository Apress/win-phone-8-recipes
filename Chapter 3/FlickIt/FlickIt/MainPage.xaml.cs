using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace FlickIt
{
    public partial class MainPage : PhoneApplicationPage
    {
        //variable used to store the touch input point 
        //that was received in the Touch_FrameReported event
        Point lastTouchedPoint = new Point(0,0);

        //used to determine if the action was an intentional flick
        const double velocityCheck = -3000;

        //checked in the Hold event so that if the action is 
        //in the middle of a drag manipulation
        //the context menu doesn't appear unwittingly
        bool ellipseInDragMode = false; 
        
        public MainPage()
        {
            InitializeComponent();            
            Touch.FrameReported += Touch_FrameReported;             
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            AddNewEllipse();            
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {            
            TouchPointCollection touchPoints = e.GetTouchPoints(this);

            //if multiple touch points were received, 
            //just get the first touch point position from the collection
            if (touchPoints.Count > 0)
            {
                
                lastTouchedPoint = touchPoints[0].Position;
            }

            //Note: if you wanted to track multiple touch input point positions, simply iterate through
            //the touchPoints collection using foreach and handle each touch input point position accordingly            
        }

        private void listMenuItems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            Ellipse targetEllipse = menuEllipse.Tag as Ellipse;
            menuEllipse.Tag = null;
            menuEllipse.IsOpen = false;

            ListBoxItem item = (ListBoxItem)e.AddedItems[0];
            switch (item.Content.ToString())
            {
                case "copy":
                    AddNewEllipse();
                    break;
                case "change color":
                    targetEllipse.Fill = EllipseManager.GetNextColor();
                    break;
                case "delete":
                    DeleteEllipse(targetEllipse);
                    break;

            }

            listMenuItems.ClearValue(ListBox.SelectedItemProperty);
        }

        private void resetButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ContentPanel.Children.Clear();
            EllipseManager.ResetCounter();
            AddNewEllipse();

            coordinatesText.Text = "X:0, Y:0";
        }

        #region GestureListener events

        private void GestureListener_Tap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            if (e.OriginalSource.GetType().Equals(typeof(Ellipse)))
            {
                if (menuEllipse.IsOpen)
                {
                    //if the context menu is open but the user taps 
                    //outside of the menu, then close it 
                    menuEllipse.IsOpen = false;
                }
                Ellipse sourceEllipse = (Ellipse)e.OriginalSource;
                sourceEllipse.Fill = EllipseManager.GetNextColor();
            }
        }

        private void GestureListener_DoubleTap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            if (e.OriginalSource.GetType().Equals(typeof(Ellipse)))
            {
                AddNewEllipse();
            }
        }

        private void GestureListener_Hold(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            
            if (!ellipseInDragMode && e.OriginalSource.GetType().Equals(typeof(Ellipse)))
            {
                Ellipse sourceEllipse = (Ellipse)e.OriginalSource;

                //if the point touched is near the right edge of the screen, adjust the 
                //HorizontalOffset for the context menu to ensure that it is displayed 
                //within the bounds of the screen
                if ((lastTouchedPoint.X + listMenuItems.Width) > this.ActualWidth)
                    lastTouchedPoint.X = this.ActualWidth - listMenuItems.Width;

                //if the point touched is near the bottom edge of the screen, adjust the 
                //VerticalOffset for the context menu to ensure that it is displayed 
                //within the bounds of the screen
                if ((lastTouchedPoint.Y + listMenuItems.Height) > this.ActualHeight)
                    lastTouchedPoint.Y = this.ActualHeight - listMenuItems.Height;

                menuEllipse.HorizontalOffset = lastTouchedPoint.X;
                menuEllipse.VerticalOffset = lastTouchedPoint.Y;
                menuEllipse.IsOpen = true;

                //set the Tag of the context menu to the selected ellipse
                //so that on menu item selection, the action that is taken
                //against the shape that triggered the event
                menuEllipse.Tag = sourceEllipse;
            }
        }


        private void GestureListener_Flick(object sender, Microsoft.Phone.Controls.FlickGestureEventArgs e)
        {
            if (e.OriginalSource.GetType().Equals(typeof(Ellipse)) &&
                    e.Direction == System.Windows.Controls.Orientation.Vertical &&
                    (e.VerticalVelocity < velocityCheck))
            {
                
                Ellipse targetEllipse = (Ellipse)e.OriginalSource;
                DeleteEllipse(targetEllipse);
            }
        }


        private void GestureListener_DragStarted(object sender, Microsoft.Phone.Controls.DragStartedGestureEventArgs e)
        {
            if (e.OriginalSource.GetType().Equals(typeof(Ellipse)))
            {
                ellipseInDragMode = true;

                Ellipse targetEllipse = (Ellipse)e.OriginalSource;

                ContentPanel.Children.ToList().ForEach(c => c.SetValue(Canvas.ZIndexProperty, 0));                
                targetEllipse.SetValue(Canvas.ZIndexProperty, 1);

                //change the border of the object that is being dragged
                targetEllipse.Stroke = new SolidColorBrush(Colors.Yellow);
            }
        }

        private void GestureListener_DragDelta(object sender, Microsoft.Phone.Controls.DragDeltaGestureEventArgs e)
        {
            if (e.OriginalSource.GetType().Equals(typeof(Ellipse)))
            {
                Ellipse targetEllipse = (Ellipse)e.OriginalSource;

                TranslateTransform transform = targetEllipse.RenderTransform as TranslateTransform;
                transform.X += e.HorizontalChange;
                transform.Y += e.VerticalChange;

                //display the last touched point's coordinates 
                //as the user drags a finger across the screen
                Point lastPosition = e.GetPosition(null);
                coordinatesText.Text = string.Format("X:{0}, Y:{1}",
                                                    lastPosition.X,
                                                    lastPosition.Y);
            }
        }

        private void GestureListener_DragCompleted(object sender, Microsoft.Phone.Controls.DragCompletedGestureEventArgs e)
        {
            if (e.OriginalSource.GetType().Equals(typeof(Ellipse)))
            {
                ellipseInDragMode = false;

                Ellipse targetEllipse = (Ellipse)e.OriginalSource;
                
                //reset the border to the default color
                targetEllipse.Stroke = new SolidColorBrush(Colors.Gray);
            }
        }
        #endregion                
        
        #region Helper Methods
        
        private void DeleteEllipse(Ellipse deleteEllipse)
        {
            //detach the event handlers and remove the selected shape
            ContentPanel.Children.Remove(deleteEllipse);
            EllipseManager.DecrementCounter();

            TextAnimation.Begin();
        }

        
        //since this set of actions will be called from multiple 
        //locations, it is ideal to place it in a separate method
        private void AddNewEllipse()
        {
            if (EllipseManager.EllipseCount < 6)
            {
                Ellipse newEllipse = EllipseManager.AddEllipse();
                ContentPanel.Children.Add(newEllipse);
            }
            else
            {
                MessageBox.Show("The screen is full!");
            }
        }

        #endregion

        
                      

    }
}