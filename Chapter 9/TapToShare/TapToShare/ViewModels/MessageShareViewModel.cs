using System.ComponentModel;
using System.Windows;
using Windows.Networking.Proximity;

namespace TapToShare.ViewModels
{
    public class MessageShareViewModel : INotifyPropertyChanged
    {        
        ProximityDevice device;
        long publishedMessageId = 0;
        long subscribedMessageId = 0;
                        
        #region Properties
        private bool isConnected = false;
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }

        private string statusMessage;
        public string StatusMessage
        {
            get
            {
                return statusMessage;
            }
            set
            {
                statusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }
        
        private string lastReceivedMessage;
        public string LastReceivedMessage
        {
            get
            {
                return lastReceivedMessage;
            }
            set
            {
                lastReceivedMessage = value;
                OnPropertyChanged("LastReceivedMessage");
            }
        }

        private string sentMessage;
        public string SentMessage
        {
            get
            {
                return sentMessage;
            }
            set
            {
                sentMessage = value;
                OnPropertyChanged("SentMessage");
            }
        }
        
        #endregion
               
        public void PublishMessage(string messageToSend)
        {
            device = ProximityDevice.GetDefault();
            if (device != null)
            {
                // Stop publishing the current message.
                if (publishedMessageId > 0)
                {
                    device.StopPublishingMessage(publishedMessageId);
                }

                publishedMessageId = device.PublishMessage("Windows.TapToShareMessage", messageToSend);
                this.SentMessage = messageToSend;
            }
        }

        public void SubscribeForMessage()
        {
            device = ProximityDevice.GetDefault();
            if (device != null)
            {
                // Only subscribe for the message one time.
                if (subscribedMessageId == 0)
                {
                    subscribedMessageId = device.SubscribeForMessage("Windows.TapToShareMessage", MessageReceived);
                }
            }
        }

        public void MessageReceived(ProximityDevice proximityDevice, ProximityMessage proximityDeviceMessage)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                LastReceivedMessage = proximityDeviceMessage.DataAsString;
            });
        }

        public void StopSubscribingForMessage()
        {
            device.StopSubscribingForMessage(subscribedMessageId);
            subscribedMessageId = 0;
        }

        public void StopPublishingForMessage()
        {
            device.StopPublishingMessage(publishedMessageId);
            publishedMessageId = 0;
        }


        #region Notify Property Changed Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    
}
