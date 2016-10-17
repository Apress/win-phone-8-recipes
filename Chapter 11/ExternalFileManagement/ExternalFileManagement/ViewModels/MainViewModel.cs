using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ExternalFileManagement.Resources;
using System.Threading.Tasks;
using Microsoft.Phone.Storage;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ExternalFileManagement.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExternalStorageFile> Files { get; set; }

        private ExternalStorageFile _selectedFile;
        public ExternalStorageFile SelectedFile
        {
            get
            {
                return _selectedFile;
            }
            set
            {
                _selectedFile = value;
                NotifyPropertyChanged("SelectedFile");
            }
        }

        private string _fileText;
        public string FileText
        {
            get
            {
                return _fileText;
            }
            set
            {
                _fileText = value;
                NotifyPropertyChanged("FileText");
            }
        }

        public MainViewModel()
        {
            this.Files = new ObservableCollection<ExternalStorageFile>();
        }

        public async Task<FilesResult> GetFiles()
        {
            FilesResult result = new FilesResult();

            ExternalStorageDevice sdCard = (await ExternalStorage.GetExternalStorageDevicesAsync()).FirstOrDefault();
            if (sdCard != null)
            {
                IEnumerable<ExternalStorageFile> files = await sdCard.RootFolder.GetFilesAsync();
                ExternalStorageFile file = files.FirstOrDefault();
                Files = new ObservableCollection<ExternalStorageFile>(files.Where(f => f.Name.EndsWith(".txt")).ToList());
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.Message = "An SD card was not detected.";

            }
            return result;
        }

        public async Task<FilesResult> LoadSelectedFile()
        {
            FilesResult result = new FilesResult();
            ExternalStorageDevice sdCard = (await ExternalStorage.GetExternalStorageDevicesAsync()).FirstOrDefault();
            if (sdCard != null)
            {
                IEnumerable<ExternalStorageFile> files = await sdCard.RootFolder.GetFilesAsync();
                if (SelectedFile.Name.EndsWith(".txt"))
                {
                    System.IO.Stream fileStream = await SelectedFile.OpenForReadAsync();

                    //Read the entire file into the FileText property
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        FileText = streamReader.ReadToEnd();
                    }

                    fileStream.Close();
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Invalid file type. Can only open text files with a '.txt' extension";
                }
            }

            return result;
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