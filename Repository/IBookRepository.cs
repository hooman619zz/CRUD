using CrudTest.Models;


namespace CrudTest.Repository
{
    public interface IBookRepository : IDisposable
    {
        public List<BookAuthorViewModel> ReadBooks(int? id);

        public Task InsertBookOnPost(BookModel book);
        public  Task<BookListViewModel> InsertBookOnGet();
        public void UpdateBookOnPost(BookModel book);

        public BookListViewModel UpdateBookOnGet(int id);

        public Task<BookModel> DeleteBookOnGet(int id);

        public Task DeleteBooksOnPost(int id);
        public void Save();
        public  Task<BookModel> GetBookById(int id);

        public IEnumerable<BookModel> GetBooks();

    }
}
