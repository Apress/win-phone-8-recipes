using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MyBookList.Resources;
using MyBookList.Data;
using System.Linq;
using System.Collections.Generic;

namespace MyBookList.ViewModels
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private BookDataContext CurrentDataContext = new BookDataContext(BookDataContext.ConnectionString);
        public ObservableCollection<BookNote> Notes { get; private set; }
        
        private BookNote _newNote;
        public BookNote NewNote
        {
            get
            {
                return _newNote;
            }
            set
            {
                _newNote = value;
                NotifyPropertyChanged("NewNote");
            }
        }

        private Book _book;
        public Book Book
        {
            get
            {
                return _book;
            }
            set
            {
                _book = value;
                NotifyPropertyChanged("Book");
            }
        }

        public BookViewModel()
        {
            this.Book = new Book();          
        }

        public BookViewModel(int bookId)
        {
            this.Book = CurrentDataContext.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            this.Book.Author = "New Author";
            CurrentDataContext.SubmitChanges();

            RefreshNotes();
        }

        private void RefreshNotes()
        {
            List<BookNote> bookNoteList = CurrentDataContext.BookNotes
                .Where(n => n.BookId == this.Book.BookId)
                .ToList();
            this.Notes = new ObservableCollection<BookNote>(bookNoteList);   
        }

        public void Save()
        {
            if (Book.BookId <= 0)
            {
                CurrentDataContext.Books.InsertOnSubmit(Book);
            }

            CurrentDataContext.SubmitChanges();
        }

        public void InitNewNote()
        {
            NewNote = new BookNote();
            NewNote.Book = this.Book;
            NewNote.Note = "";
        }

        public void AddNewNote()
        {
            CurrentDataContext.BookNotes.InsertOnSubmit(NewNote);
            CurrentDataContext.SubmitChanges();
            RefreshNotes();
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