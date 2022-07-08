using CrudTest.Models;
//using CrudTest.Data;


namespace CrudTest.Repository
{
    public interface IBookRepository : IDisposable
    {
        public List<BookAuthorViewModel> ReadBooks(int? id);

        public void InsertBookOnPost(BookModel book);
        public  Task<BookListViewModel> InsertBookOnGet();
        public void UpdateBookOnPost(BookModel book);

        public BookListViewModel UpdateBookOnGet(int id);

        public Task<BookModel> DeleteBookOnGet(int id);

        public void DeleteBooksOnPost(int id);
        public void Save();
    }
}
