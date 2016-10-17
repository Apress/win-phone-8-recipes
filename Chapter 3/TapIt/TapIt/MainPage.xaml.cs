using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TapIt
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        //variable used to store the touch input point 
        //that was received in the Touch_FrameReported event
        Point lastTouchedPoint = new Point(0,0);

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

        
        private void Ellipse_Held(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (sender.GetType().Equals(typeof(Ellipse)))
            {
                Ellipse sourceEllipse = (Ellipse)sender;

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

        private void Ellipse_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (menuEllipse.IsOpen)
            {
                //if the context menu is open but the user taps 
                //outside of the menu, then close it 
                menuEllipse.IsOpen = false;
            }
            Ellipse sourceEllipse = (Ellipse)sender;
            sourceEllipse.Fill = EllipseManager.GetNextColor();
        }

        private void Ellipse_DoubleTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AddNewEllipse();
        }
        
        private void listMenuItems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            Ellipse sourceEllipse = menuEllipse.Tag as Ellipse;
            menuEllipse.Tag = null;
            menuEllipse.IsOpen = false;

            ListBoxItem item = (ListBoxItem)e.AddedItems[0];
            switch (item.Content.ToString())
            {
                case "add circle":
                    AddNewEllipse();
                    break;
                case "change color":
                    sourceEllipse.Fill = EllipseManager.GetNextColor();
                    break;
                case "delete":
                    RemoveEventHandlers(sourceEllipse);
                    EllipseManager.DeleteEllipse(sourceEllipse);
                    ContentPanel.Children.Remove(sourceEllipse);
                    break;

            }

            //reset the state of the listbox so that the selected item 
            //no longer appears to be selected on the next display of the menu
            listMenuItems.ClearValue(ListBox.SelectedItemProperty);
        }

        //since this set of actions will be called from multiple 
        //locations, it is ideal to place it in a separate method
        private void AddNewEllipse()
        {
            if (EllipseManager.EllipseCount < 6)
            {
                Ellipse newEllipse = EllipseManager.AddEllipse();
                AttachEventHandlers(newEllipse);
                ContentPanel.Children.Add(newEllipse);
            }
            else
            {
                MessageBox.Show("The screen is full!");
            }
        }

        private void AttachEventHandlers(Ellipse targetEllipse)
        {
            targetEllipse.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(Ellipse_Tapped);
            targetEllipse.DoubleTap += new EventHandler<System.Windows.Input.GestureEventArgs>(Ellipse_DoubleTapped);
            targetEllipse.Hold += new EventHandler<System.Windows.Input.GestureEventArgs>(Ellipse_Held);
        }

        
        private void RemoveEventHandlers(Ellipse targetEllipse)
        {
            targetEllipse.Tap -= new EventHandler<System.Windows.Input.GestureEventArgs>(Ellipse_Tapped);
            targetEllipse.DoubleTap -= new EventHandler<System.Windows.Input.GestureEventArgs>(Ellipse_DoubleTapped);
            targetEllipse.Hold -= new EventHandler<System.Windows.Input.GestureEventArgs>(Ellipse_Held);
        }


    }
}