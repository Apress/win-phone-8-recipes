using Microsoft.Xna.Framework.Media;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace TapToShare.ViewModels
{
    public class PictureShareViewModel : INotifyPropertyChanged
    {
        private DataWriter dataWriter;
        private DataReader dataReader;

        StreamSocket socket;
        ProximityDevice device;

        public PictureShareViewModel()
        {
            if ((PeerFinder.SupportedDiscoveryTypes & PeerDiscoveryTypes.Triggered) == PeerDiscoveryTypes.Triggered)
            {
                //Register for incoming connection requests
                PeerFinder.TriggeredConnectionStateChanged += PeerFinder_TriggeredConnectionStateChanged;
            }
        }


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

        private BitmapImage selectedPicture;
        public BitmapImage SelectedPicture
        {
            get
            {
                return selectedPicture;
            }
            set
            {
                selectedPicture = value;
                OnPropertyChanged("SelectedPicture");
            }
        }
        #endregion

        void PeerFinder_TriggeredConnectionStateChanged(object sender, TriggeredConnectionStateChangedEventArgs args)
        {
            device = ProximityDevice.GetDefault();

            switch (args.State)
            {
                case TriggeredConnectState.Canceled:
                    //This may have been triggered accidentally
                    //so just ignore it. There is a way the user can 
                    //forcibly cancel the connection through the UI
                    break;
                case TriggeredConnectState.Completed:
                    this.IsConnected = true;

                    socket = args.Socket;
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        StartRandomPictureShare();
                    });
                    SetStatus(args.State.ToString());

                    //Connection established, so we can stop advertising now
                    PeerFinder.Stop();
                    break;
                case TriggeredConnectState.Listening:
                    this.IsConnected = true;

                    socket = args.Socket;
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        GetRandomPictureShare();
                    });
                    SetStatus(args.State.ToString());

                    //Connection established, so we can stop advertising now
                    PeerFinder.Stop();
                    break;
                default:
                    //set the connected flag to false and display the state in the UI
                    SetStatus(args.State.ToString());
                    this.IsConnected = false;
                    break;

            }
        }

        private void SetStatus(string status)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.StatusMessage = status;
            });

        }

        public void StartAdvertising()
        {
            PeerFinder.Start();
        }

        public void CloseConnection()
        {
            if (socket != null)
                socket.Dispose();

            if (dataReader != null)
                dataReader.Dispose();

            if (dataWriter != null)
                dataWriter.Dispose();

            SetStatus("Connection closed. Sharing ended");
        }

        public async void SendPicture(byte[] imageToSend)
        {
            if (this.IsConnected)
            {
                //get the device that is in close proximity 
                ProximityDevice device = ProximityDevice.GetDefault();

                //if device is not null, then NFC is supported on that device, 
                //so we can continue to with the picture transmission
                if (device != null &&
                    socket != null &&
                    socket.OutputStream != null)
                {
                    dataWriter = new DataWriter(socket.OutputStream);

                    dataWriter.WriteInt32(imageToSend.Length);
                    await dataWriter.StoreAsync();

                    dataWriter.WriteBytes(imageToSend);
                    await dataWriter.StoreAsync();

                }
            }
        }

        public void StartRandomPictureShare()
        {
            byte[] imageToSend;

            Stream pictureStream;
            BitmapImage bmp;

            if (this.IsConnected)
            {
                MediaLibrary mediaLib = new MediaLibrary();
                int picsCount = mediaLib.Pictures.Count;
                if (picsCount > 0)
                {
                    Random random = new Random();
                    int nextPic = random.Next(1, picsCount);
                    pictureStream = mediaLib.Pictures[nextPic].GetImage();

                    bmp = new BitmapImage();
                    bmp.SetSource(pictureStream);

                    this.SelectedPicture = bmp;

                    MemoryStream ms = new MemoryStream();
                    pictureStream.Seek(0, SeekOrigin.Begin);
                    pictureStream.CopyTo(ms);

                    imageToSend = ms.ToArray();

                    SendPicture(imageToSend);

                }
            }
            else
            {
                this.StatusMessage = "Connection not established. Failed to share picture.";
            }
        }

        public async void GetRandomPictureShare()
        {
            if (this.IsConnected)
            {
                byte[] imageReceived;

                //get the device that is in close proximity 
                ProximityDevice device = ProximityDevice.GetDefault();

                //if device is not null, then NFC is supported on that device, 
                //so we can continue with the picture transmission
                if (device != null &&
                    socket != null &&
                    socket.InputStream != null)
                {
                    dataReader = new DataReader(socket.InputStream);

                    await dataReader.LoadAsync(4);
                    uint messageLen = (uint)dataReader.ReadInt32();

                    imageReceived = new byte[messageLen];

                    await dataReader.LoadAsync(messageLen);
                    dataReader.ReadBytes(imageReceived);

                    MemoryStream ms = new MemoryStream(imageReceived);
                    BitmapImage bmp = new BitmapImage();
                    bmp.SetSource(ms);
                    this.SelectedPicture = bmp;

                }
            }
            else
            {
                this.StatusMessage = "Connection lost. Failed to receive shared picture.";
            }
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
