using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace MyBluetoothChatApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public event EventHandler<ConnectionRequestedEventArgs> ChatConnectionRequested;
        public event EventHandler<ChatErrorEventArgs> ChatErrorOccurred;
        public event EventHandler<FindPeersEventArgs> FindPeersCompleted;
        
        private DataWriter dataWriter;
        private DataReader dataReader;

        StreamSocket socket;
        string appName = "My Bluetooth Chat App";                
        
        public ChatViewModel()
        {
            //Register for incoming connection requests
            PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;

            //Advertise the app so that our peers can find it
            PeerFinder.DisplayName = appName;
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

        private string peerName = "unknown";
        public string PeerName
        {
            get
            {
                return peerName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    peerName = value;
                    OnPropertyChanged("PeerName");
                }
            }
        }

        private string profileName = "me";
        public string ProfileName
        {
            get
            {
                return profileName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    profileName = value;
                    OnPropertyChanged("ProfileName");
                }
            }
        }

        private ObservableCollection<string> chatMessages = new ObservableCollection<string>();
        public ObservableCollection<string> ChatMessages
        {
            get
            {
                return chatMessages;
            }
            set
            {
                chatMessages = value;
                OnPropertyChanged("ChatMessages");
            }
        }

        private ObservableCollection<PeerInformation> peerAppsList = new ObservableCollection<PeerInformation>();
        public ObservableCollection<PeerInformation> PeerAppsList
        {
            get
            {
                return peerAppsList;
            }
            set
            {
                peerAppsList = value;
                OnPropertyChanged("PeerAppsList");
            }
        }
        #endregion

        public void StartAdvertising()
        {
            PeerFinder.Start();
            FindPeers();
        }

        public void EndChat()
        {
            PeerFinder.ConnectionRequested -= PeerFinder_ConnectionRequested;
            CloseConnection();
        }
        
        public async void Connect(PeerInformation peer)
        {
            try
            {
                socket = await PeerFinder.ConnectAsync(peer);

                //now that we are connected, stop advertising to save battery life
                PeerFinder.Stop();

                this.PeerName = peer.DisplayName;
                this.IsConnected = true;
                this.ChatMessages.Add("chat started with " + this.PeerName);

                ListenForIncomingMessage();
            }
            catch (Exception ex)
            {
                if (this.ChatErrorOccurred != null)
                {
                    this.ChatErrorOccurred(this, new ChatErrorEventArgs { ErrorMessage = ex.Message });
                }
            }
        }

        public async void FindPeers()
        {
            try
            {
                IReadOnlyList<PeerInformation> peers = await PeerFinder.FindAllPeersAsync();
                
                this.PeerAppsList = new ObservableCollection<PeerInformation>();
                foreach (PeerInformation info in peers)
                {
                    this.PeerAppsList.Add(info);
                }

                OnPropertyChanged("PeerAppsList");

                if (this.FindPeersCompleted != null)
                {
                    this.FindPeersCompleted(this, new FindPeersEventArgs());
                }
            }
            catch (Exception ex)
            {
                FindPeersEventArgs args = new FindPeersEventArgs();
                args.IsBluetoothOff = ((uint)ex.HResult == 0x8007048F);
                args.ErrorOccurred = true;
                args.Message = ex.Message;

                if (this.FindPeersCompleted != null)
                {
                    this.FindPeersCompleted(this, args);
                }
            }

        }

        public async void SendMessage(string message)
        {
            if (socket != null && message.Trim().Length > 0)
            {
                if (dataWriter == null)
                    dataWriter = new DataWriter(socket.OutputStream);
                                
                //Send the message length first
                dataWriter.WriteInt32(message.Length);
                await dataWriter.StoreAsync();

                //Next, send the actual message
                dataWriter.WriteString(message);
                await dataWriter.StoreAsync();

                message = FormatMessage(this.ProfileName, message);
                this.ChatMessages.Add(message);
            }
        }
        
        private async void ListenForIncomingMessage()
        {
            try
            {
                var message = await GetMessage();

                message = FormatMessage(this.PeerName, message);
                this.ChatMessages.Add(message);

                //Listen for the next message
                ListenForIncomingMessage();
            }
            catch (Exception)
            {
                CloseConnection();
            }
        }

        private async Task<string> GetMessage()
        {
            if (dataReader == null)
                dataReader = new DataReader(socket.InputStream);

            //Get the size of the message
            await dataReader.LoadAsync(4);
            uint messageLen = (uint)dataReader.ReadInt32();

            //Get the actual message
            await dataReader.LoadAsync(messageLen);

            return dataReader.ReadString(messageLen);
        }

        private void PeerFinder_ConnectionRequested(object sender, ConnectionRequestedEventArgs args)
        {
            if (this.ChatConnectionRequested != null)
            {
                this.ChatConnectionRequested(this, args);
            }
        }

        private string FormatMessage(string name, string message)
        {
            return name + ": " + message;
        }

        private void CloseConnection()
        {
            this.ChatMessages.Add("chat ended");
            this.IsConnected = false;

            DisposeObject(dataReader);
            DisposeObject(dataWriter);
            DisposeObject(socket);

        }

        private void DisposeObject(IDisposable currentObject)
        {
            if (currentObject != null)
            {
                currentObject.Dispose();
                currentObject = null;
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

    public class ChatErrorEventArgs : EventArgs
    {
        public string ErrorMessage { get; set; }
    }

    public class FindPeersEventArgs : EventArgs
    {
        public bool IsBluetoothOff { get; set; }
        public bool ErrorOccurred { get; set; }
        public string Message { get; set; }
        
    }

}
