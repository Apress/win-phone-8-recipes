using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using MyBluetoothChatApp.ViewModels;

namespace MyBluetoothChatApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ChatViewModel chatViewModel;

        public MainPage()
        {
            InitializeComponent();

            chatViewModel = new ChatViewModel();
            chatViewModel.ChatConnectionRequested += chatViewModel_ChatConnectionRequested;
            chatViewModel.FindPeersCompleted += chatViewModel_FindPeersCompleted;
            this.DataContext = chatViewModel;            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            chatViewModel.StartAdvertising();
        }

        private void chatViewModel_FindPeersCompleted(object sender, FindPeersEventArgs e)
        {
            if (e.ErrorOccurred)
            {
                if (e.IsBluetoothOff)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Bluetooth is turned off. Would you like to turn it on?", 
                        "My Bluetooth Chat App", 
                        MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        ConnectionSettingsTask connectionSettingsTask = new ConnectionSettingsTask();
                        connectionSettingsTask.ConnectionSettingsType = ConnectionSettingsType.Bluetooth;
                        connectionSettingsTask.Show();
                    }
                }
                else
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                peerStatus.Text = (chatViewModel.PeerAppsList.Count == 0) ? 
                    "no peers found" :
                    "select a peer to connect with";
                
            }
        }

        private void chatViewModel_ChatConnectionRequested(object sender, ConnectionRequestedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                        string.Format("{0} is trying to connect. Would you like to accept?", e.PeerInformation.DisplayName),
                        "My Bluetooth Chat App",
                        MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                chatViewModel.Connect(e.PeerInformation);
            }
        }
        
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            chatViewModel.EndChat();
        }
                
        private void SendMessage_Tap(object sender, GestureEventArgs e)
        {
            chatViewModel.SendMessage(chatMessage.Text);
            chatMessage.Text = "";
        }

        private void EndChat_Tap(object sender, GestureEventArgs e)
        {
            chatViewModel.EndChat();
            chatViewModel.StartAdvertising();
            chatPivot.SelectedIndex = 0;
        }

        private void ChatPivot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            PivotItem item = e.AddedItems[0] as PivotItem;
            if (!chatViewModel.IsConnected)
            {
                if (item == chatPivotItem)
                {
                    MessageBox.Show("You must connect first before chatting");
                }

                chatViewModel.ChatMessages.Clear();
            }
        }

        private void PeerLongListSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            PeerInformation peer = e.AddedItems[0] as PeerInformation;
            chatViewModel.Connect(peer);
            chatPivot.SelectedIndex = 1;
        }

        private void FindPeers_Tap(object sender, GestureEventArgs e)
        {
            peerStatus.Text = "attempting to find peers...";
            chatViewModel.FindPeers();
        }    
    }

}
