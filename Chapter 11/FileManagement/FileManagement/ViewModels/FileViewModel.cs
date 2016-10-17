using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FileManagement.ViewModels
{
    public class FileViewModel : INotifyPropertyChanged
    {
        private bool _isNew;
        public bool IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;

                if (_isNew)
                {
                    this.FileName = "";
                    this.FileText = "";                
                }

                NotifyPropertyChanged("IsNew");
            }
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                NotifyPropertyChanged("FileName");
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

        public async Task<bool> LoadFile(string fileName)
        {
            this.FileName = fileName;

            //Get the local folder for the current application
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (folder != null)
            {
                try
                {                    
                    //Load the specified file
                    Stream file = await folder.OpenStreamForReadAsync(this.FileName);

                    //Read the entire file into the FileText property
                    using (StreamReader streamReader = new StreamReader(file))
                    {
                        FileText = streamReader.ReadToEnd();
                    }

                    file.Close();
                }
                catch (FileNotFoundException ex)
                {
                    //file doesn't exist
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> SaveFile()
        {
            try
            {                
                //Get the local folder for the current application
                StorageFolder folder = ApplicationData.Current.LocalFolder;

                if (folder != null)
                {
                    //Create a new file, or update file if one already exists with the same name
                    StorageFile file = await folder.CreateFileAsync(this.FileName, CreationCollisionOption.ReplaceExisting);

                    //Convert the file text to a byte array 
                    byte[] fileBytes = Encoding.UTF8.GetBytes(FileText.ToCharArray());
                    
                    //Write the file contents using a StreamWriter
                    Stream fileStream = await file.OpenStreamForWriteAsync();
                    fileStream.Write(fileBytes, 0, fileBytes.Length);
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
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
