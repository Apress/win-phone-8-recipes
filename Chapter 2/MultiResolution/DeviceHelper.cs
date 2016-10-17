using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicMultiResolution
{
    public static class DeviceHelper
    {
        private static int scaleFactor = Application.Current.Host.Content.ScaleFactor;
        private static Resolution resolution;

        static DeviceHelper()
        {
            switch(scaleFactor)
            {
                case 100:
                    resolution = Resolution.WVGA;
                    break;
                case 160:
                    resolution = Resolution.WXGA;
                    break;
                case 150:
                    resolution = Resolution.HD720p;
                    break;
                default:
                    throw new InvalidOperationException(string.Format("The scale factor {0} is not supported", scaleFactor));
                    break;
            }
        }
        public static Resolution Resolution
        {
            get
            {
                return resolution;
            }
        }
    }
}
