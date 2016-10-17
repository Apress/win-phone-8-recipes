using Microsoft.Phone.Data.Linq;
using Microsoft.Phone.Data.Linq.Mapping;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace MyBookList.Data
{
    [Table(Name = "Book")]
    public class Book : INotifyPropertyChanged, INotifyPropertyChanging
    {
            #region Constructor
            public Book()
            {
            
                _notes = new EntitySet<BookNote>(
                        note =>
                        {
                            NotifyPropertyChanging("BookNotes");
                            note.Book = this;
                        },
                        note =>
                        {
                            NotifyPropertyChanging("BookNotes");
                            note.Book = null;
                        }
                    );
            }
            #endregion
        
            #region Columns
            private int _bookId;
            [Column(IsPrimaryKey = true,IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
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

            private string _title;
            [Column(DbType = "nvarchar(255)", CanBeNull = false)]
            public string Title
            {
                get { return _title; }
                set
                {
                    if (_title != value)
                    {
                        NotifyPropertyChanging("Title");
                        _title = value;
                        NotifyPropertyChanged("Title");
                    }
                }
            }

            private string _author;
            [Column(DbType = "nvarchar(255)", CanBeNull = false)]
            public string Author
            {
                get { return _author; }
                set
                {
                    if (_author != value)
                    {
                        NotifyPropertyChanging("Author");
                        _author = value;
                        NotifyPropertyChanged("Author");
                    }
                }
            }        
        
            #endregion
        
            #region Book Notes Association (Child Table)
            private EntitySet<BookNote> _notes;
            [Association(Name = "FK_Book_Notes", Storage = "_notes", ThisKey = "BookId", OtherKey = "BookId")]
            public EntitySet<BookNote> Notes
            {
                get
                {
                    return _notes;
                }
                set
                {
                    _notes.Assign(value);
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
