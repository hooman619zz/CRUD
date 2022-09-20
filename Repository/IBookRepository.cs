using CrudTest.Models;


namespace CrudTest.Repository
{
    public interface IBookRepository :IGenericRepository<BookModel>, IDisposable
    {
        public List<BookAuthorViewModel> ReadBooks(int? id);

        public  Task<BookListViewModel> InsertBookOnGet();

        public BookListViewModel UpdateBookOnGet(int id);

        public Task<BookModel> DeleteBookOnGet(int id);

        public void Save();

        public IEnumerable<BookModel> GetBooks();

    }
}
