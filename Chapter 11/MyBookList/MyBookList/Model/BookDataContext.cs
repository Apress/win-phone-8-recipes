using System.Data.Linq;

namespace MyBookList.Data
{
    public class BookDataContext : DataContext
    {
        public const string ConnectionString = "isostore:/Books.sdf";

        public BookDataContext(string connectionString)
            : base(connectionString)
        {
            this.Books = this.GetTable<Book>();
            this.BookNotes = this.GetTable<BookNote>();
        }

        public Table<Book> Books { get; set; }
        public Table<BookNote> BookNotes { get; set; }
    }
}
