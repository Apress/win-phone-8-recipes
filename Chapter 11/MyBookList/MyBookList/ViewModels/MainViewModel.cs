using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MyBookList.Resources;
using MyBookList.Data;
using System.Linq;
using System.Collections.Generic;

namespace MyBookList.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private BookDataContext CurrentDataContext;
        public ObservableCollection<Book> Books { get; private set; }
        public bool IsDataLoaded { get; private set; }

        public MainViewModel()
        {
            this.Books = new ObservableCollection<Book>();
            CurrentDataContext = new BookDataContext(BookDataContext.ConnectionString);

            if (!CurrentDataContext.DatabaseExists())
            {
                CurrentDataContext.CreateDatabase();
                CurrentDataContext.SubmitChanges();
            }
        }
                
        public void LoadBooks()
        {
            if (CurrentDataContext.Books.Count() > 0)
            {
                List<Book> bookList = CurrentDataContext.Books.ToList();
                Books = new ObservableCollection<Book>(bookList);
            }
            this.IsDataLoaded = true;
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