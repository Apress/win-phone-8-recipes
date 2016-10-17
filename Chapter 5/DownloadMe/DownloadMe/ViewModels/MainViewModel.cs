using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Phone.BackgroundTransfer;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace DownloadMe.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _downloadsInProgress = 0;
        
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("Items"))
            {
                this.Items = IsolatedStorageSettings.ApplicationSettings["Items"] as ObservableCollection<ItemViewModel>;
                NotifyPropertyChanged("Items");

                ReinitTransferRequests();
            }
            else
            {
                this.Items.Add(new ItemViewModel()
                {
                    ID = "1",
                    Title = "Building Windows Phone Apps",
                    Speaker = "Lori Lalonde",
                    DownloadUrl = "http://media.ch9.ms/ch9/a2ec/8673d554-4992-48d4-80e7-b9acdda4a2ec/DEVC3.wmv"
                });

                this.Items.Add(new ItemViewModel()
                {
                    ID = "2",
                    Title = "Code Once And Share",
                    Speaker = "Alnur Ismail",
                    DownloadUrl = "http://media.ch9.ms/ch9/e394/db1675cf-8305-4638-b05c-d0b3e605e394/DEVC4.wmv"
                });

                this.Items.Add(new ItemViewModel()
                {
                    ID = "3",
                    Title = "That Which Connects Them All",
                    Speaker = "Bruce Johnson",
                    DownloadUrl = "http://media.ch9.ms/ch9/8543/1fa068cb-8042-4adb-95bb-c8494ee68543/DEVC5.wmv"
                });
                this.Items.Add(new ItemViewModel()
                {
                    ID = "4",
                    Title = "Building A Windows Store App UI",
                    Speaker = "Atley Hunter",
                    DownloadUrl = "http://media.ch9.ms/ch9/fcf6/a6c754c6-94c8-4a44-8d23-ae1f2781fcf6/DEVC2.wmv"
                });
            }
            
            this.IsDataLoaded = true;
        }

        public void DownloadSelectedItems()
        {
            List<ItemViewModel> selectedItems = this.Items.Where(i => i.ItemSelected).ToList();

            _downloadsInProgress = selectedItems.Count;

            //register a background request for each media item separately
            foreach (ItemViewModel item in selectedItems)
            {
                Uri videoUri = new Uri(item.DownloadUrl, UriKind.Absolute);                
                Uri downloadLocationUri = new Uri("shared/transfers/" + videoUri.Segments.Last(), UriKind.RelativeOrAbsolute);

                //add the request for the selected item to the service if it has not already been added
                if (BackgroundTransferService.Requests.Where(r => r.RequestUri == videoUri).FirstOrDefault() == null)
                {
                    BackgroundTransferRequest request = new BackgroundTransferRequest(videoUri, downloadLocationUri);
                    request.TransferPreferences = TransferPreferences.AllowCellularAndBattery;

                    //register the background request with the operating system
                    BackgroundTransferService.Add(request);

                    item.LocalUri = "";
                    item.DownloadProgress = "downloading video...";

                    //register these events for each request to monitor download progress and status
                    request.TransferProgressChanged += new EventHandler<BackgroundTransferEventArgs>(request_TransferProgressChanged);
                    request.TransferStatusChanged += new EventHandler<BackgroundTransferEventArgs>(request_TransferStatusChanged);
                }
            }

            SaveToIsolatedStorage();
        }

        public void ReinitTransferRequests()
        {

            foreach (BackgroundTransferRequest request in BackgroundTransferService.Requests)
            {
                if (request.TransferStatus == TransferStatus.Completed)
                {
                    //if a queued up request has completed when the app was not running
                    //be sure to reflect the completed state for the item in the collection 
                    TransferCompleted(request);
                }
                else
                {
                    //if we have active requests in the BackgroundTransferService, rewire the events
                    request.TransferProgressChanged += new EventHandler<BackgroundTransferEventArgs>(request_TransferProgressChanged);
                    request.TransferStatusChanged += new EventHandler<BackgroundTransferEventArgs>(request_TransferStatusChanged);
                }

            }
        }


        void request_TransferProgressChanged(object sender, BackgroundTransferEventArgs e)
        {
            //calculate the progress percentage so that we can display it in the UI
            double progress = (e.Request.BytesReceived * 100) / e.Request.TotalBytesToReceive;

            //get the current item from the collection that is being downloaded by this request
            //this can be determined by matching the item's DownloadUrl to the RequestUri
            ItemViewModel currentItem = this.Items
                .Where(i => i.DownloadUrl == e.Request.RequestUri.AbsoluteUri).FirstOrDefault();
            if (currentItem != null)
            { 
                //update the DownloadProgress property so that it is reflected in the control that it is bound to in the UI
                currentItem.DownloadProgress = string.Format("download progress: {0}%", progress.ToString());
            }
        }

        void request_TransferStatusChanged(object sender, BackgroundTransferEventArgs e)
        {
            BackgroundTransferRequest currentRequest = e.Request;
            if (currentRequest.TransferStatus == TransferStatus.Completed)
            {
                TransferCompleted(currentRequest);                
            }
        }

        private void TransferCompleted(BackgroundTransferRequest currentRequest)
        {
            //get the current item from the collection that is being downloaded by this request
            //this can be determined by matching the item's DownloadUrl to the RequestUri
            ItemViewModel currentItem = this.Items
                .Where(i => i.DownloadUrl == currentRequest.RequestUri.AbsoluteUri).FirstOrDefault();

            if (currentItem != null)
            {
                if (currentRequest.TransferError != null)
                {
                    //update the DownloadProgress to indicate the download failed
                    //and include the error message received in the TransferError object on the request
                    currentItem.DownloadProgress = "download failed: " + currentRequest.TransferError.Message;
                }
                else
                {
                    currentItem.DownloadProgress = "";
                    currentItem.ItemDownloaded = true;

                    //format the download location Uri to use forward slashes instead so that when 
                    //the user attempts to play the media files in the UI, the file system location is recognized
                    string[] downloadUri = currentRequest.DownloadLocation.OriginalString.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    currentItem.LocalUri = string.Join("/", downloadUri);
                    currentItem.ItemSelected = false;
                }
            }

            //unregister the events from the request
            currentRequest.TransferProgressChanged -= request_TransferProgressChanged;
            currentRequest.TransferStatusChanged -= request_TransferStatusChanged;

            //remove the request from the BackgroundTransferService
            BackgroundTransferService.Remove(currentRequest);
            _downloadsInProgress -= 1;

            if (_downloadsInProgress == 0)
            {
                SaveToIsolatedStorage();
            }
        }

        public void SaveToIsolatedStorage()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("Items"))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove("Items");
            }

            IsolatedStorageSettings.ApplicationSettings.Add("Items", this.Items);
            IsolatedStorageSettings.ApplicationSettings.Save();          
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}