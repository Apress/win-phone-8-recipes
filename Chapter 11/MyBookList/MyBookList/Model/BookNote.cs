using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace MyBookList.Data
{
    [Table(Name = "BookNote")]
    public class BookNote : INotifyPropertyChanged, INotifyPropertyChanging
    {        
        #region Columns
        private int _bookNoteId;
        [Column(IsPrimaryKey = true,IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int BookNoteId
        {
            get { return _bookNoteId; }
            set
            {
                if (_bookNoteId != value)
                {
                    NotifyPropertyChanging("BookNoteId");
                    _bookNoteId = value;
                    NotifyPropertyChanged("BookNoteId");
                }
            }

        }

        private string _note;
        [Column(DbType = "nvarchar(255)", CanBeNull = false)]
        public string Note
        {
            get { return _note; }
            set
            {
                if (_note != value)
                {
                    NotifyPropertyChanging("Note");
                    _note = value;
                    NotifyPropertyChanged("Note");
                }
            }
        }

        private DateTime _dateCreated = DateTime.Now;
        [Column(CanBeNull = false)]
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
            set
            {
                if (value != _dateCreated)
                {
                    NotifyPropertyChanging("DateCreated");
                    _dateCreated = value;
                    NotifyPropertyChanged("DateCreated");
                }
            }
        }

        internal int _bookId;
        [Column(CanBeNull = false)]
        public int BookId
        {
            get { return _bookId; }
            set
            {
                if (_bookId != value)
                {
                    NotifyPropertyChanging("BookId");
                    _bookId = value;
                    NotifyPropertyChanged("BookId");
                }
            }
        }
        #endregion

        #region Parent Table Association
        private EntityRef<Book> _book;
        [Association(Name = "FK_Book_Notes", Storage = "_book", ThisKey = "BookId", OtherKey = "BookId", IsForeignKey = true)]
        public Book Book
        {
            get
            {
                return _book.Entity;
            }
            set
            {
                NotifyPropertyChanging("Book");
                _book.Entity = value;

                if (value != null)
                {
                    _bookId = value.BookId;
                }

                NotifyPropertyChanged("Book");

            }
        }
        #endregion

        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

}
