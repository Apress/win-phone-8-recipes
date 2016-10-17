using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace FlickIt
{
    static class EllipseManager
    {
        //random number generator used to generate the RGB values for a color
        private static Random random = new Random();

        private static int ellipseCount = 0;
        public static int EllipseCount
        {
            get
            {
                return ellipseCount;
            }
        }

        public static Ellipse AddEllipse()
        {            
            Ellipse newEllipse = new Ellipse();
            newEllipse.Stroke = new SolidColorBrush(Colors.Gray);
            newEllipse.StrokeThickness = 5;
            newEllipse.Height = 200;
            newEllipse.Width = 200;
            newEllipse.Margin = new Thickness(10);
            newEllipse.Fill = GetNextColor();
            newEllipse.HorizontalAlignment = HorizontalAlignment.Left;
            newEllipse.VerticalAlignment = VerticalAlignment.Top;
            newEllipse.RenderTransform = new TranslateTransform();
            newEllipse.SetValue(Canvas.ZIndexProperty, 0);
            ellipseCount += 1;
            
            return newEllipse;
        }

        public static void DecrementCounter()
        {
            ellipseCount -= 1;
        }

        public static void ResetCounter()
        {
            ellipseCount = 0;
        }

        public static SolidColorBrush GetNextColor()
        {
            Color newColor = new Color();
            newColor.A = (byte)255;
            newColor.R = (byte)random.Next(0, 256);
            newColor.G = (byte)random.Next(0, 256);
            newColor.B = (byte)random.Next(0, 256);
            return new SolidColorBrush(newColor);
        }

    }
}
