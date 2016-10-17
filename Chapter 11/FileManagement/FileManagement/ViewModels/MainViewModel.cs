using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace FileManagement.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
        }

        public ObservableCollection<StorageFile> Files { get; set; }
                
        public async Task GetFiles()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            
            IReadOnlyCollection<StorageFile> files = await folder.GetFilesAsync();
            Files = new ObservableCollection<StorageFile>(files.OrderBy(f => f.DateCreated));

        }


    }
}
