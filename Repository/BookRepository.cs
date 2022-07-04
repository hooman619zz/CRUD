using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Repository
{
    public class BookRepository : IBookRepository
    {
        private ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #region insert book

        public BookListViewModel InsertBookOnGet()
        {
            var authors = _context.Authors.ToList();
            var libraries = _context.Libraries.ToList();
            BookListViewModel bookListViewModel = new BookListViewModel()
            {
                AuthorModel = authors,
                LibraryModel = libraries
            };
            return bookListViewModel;
        }
        public async void InsertBookOnPost(BookModel book)
        {
            await _context.Books.AddAsync(book);
        }
        #endregion
        #region book list
        /// <summary>
        /// in method list ketab haye shomara midahad
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ICollection<BookModel> ReadBooks(int? id)
        {
            if (id != null)
            {
                var books = _context.Libraries.Include(b => b.BookModels).Where(l => l.Id == id)
                                  .Select(s => s.BookModels).SingleOrDefault();
                return books;


            }
            else
            {
                var Books = _context.Books.ToList();
                return Books;
            }
        }
        #endregion
        #region save
        public void Save()
        {
            _context.SaveChanges();
        }


        #endregion
        #region update (edit)

        public BookListViewModel UpdateBookOnGet(int id)
        {
            var book = _context.Books
                            .Where(b => b.Id == id)
                             .Select(s => new BookModel()
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 AuthorId = s.AuthorId,
                                 AuthorModel = s.AuthorModel,
                                 Publisher = s.Publisher,
                                 ISBN = s.ISBN,
                                 Quantity = s.Quantity,


                             }).IgnoreQueryFilters().FirstOrDefault();
            var authors = _context.Authors.ToList();

            BookListViewModel bookListViewModel = new BookListViewModel()
            {
                BookModel = book,
                AuthorModel = authors
            };
            return bookListViewModel;
        }



        /// <summary>
        /// in method baray edit kardane ketab ha be kar miravad
        /// </summary>
        /// <param name="book"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateBookOnPost(BookModel bookModel)
        {
            var book = _context.Books.Find(bookModel.Id);
            if (bookModel != null)
            {
                book.Name = bookModel.Name;
                book.ISBN = bookModel.ISBN;
                book.Publisher = bookModel.Publisher;
                book.AuthorId = bookModel.AuthorId;
                book.Quantity = bookModel.Quantity;

            }
        }
        #endregion
        #region Delete
        public BookModel DeleteBookOnGet(int id)
        {
            var book = _context.Books
                    .Where(b => b.Id == id)
                     .Select(s => new BookModel()
                     {

                         Id = s.Id,
                         Name = s.Name,
                         AuthorId = s.AuthorId,
                         AuthorModel = s.AuthorModel,
                         Publisher = s.Publisher,
                         ISBN = s.ISBN,
                         Quantity = s.Quantity,


                     }).IgnoreQueryFilters().FirstOrDefault();
            return book;
        }

        public void DeleteBooksOnPost(int id)
        {
            var book = _context.Books.Find(id);
            _context.Books.Remove(book);
        }
        #endregion
    }
}
