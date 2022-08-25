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

        public async Task<BookListViewModel> InsertBookOnGet()
        {
            var authors = await _context.Authors.ToListAsync();
            var libraries = await _context.Libraries.ToListAsync();
            BookListViewModel bookListViewModel = new BookListViewModel()
            {
                AuthorModel = authors,
                LibraryModel = libraries
            };
            return bookListViewModel;
        }
        public async void InsertBookOnPost(BookModel book)
        {
            Random rnd = new Random();
            int rate = rnd.Next(5);
            int price = rnd.Next(1, 1000);
            book.Rate = rate;
            book.Price = price;
            await _context.Books.AddAsync(book);
        }
        #endregion
        #region book list
        /// <summary>
        /// in method list ketab haye shomara midahad
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<BookAuthorViewModel> ReadBooks(int? id)
        {
            List<BookAuthorViewModel> bookAuthorViewModels = new List<BookAuthorViewModel>();
            if (id != null)
            {
                var Books = _context.Libraries.Include(b => b.BookModels).Where(l => l.Id == id)
                                  .Select(s => s.BookModels).SingleOrDefault();
                foreach (var item in Books)
                {
                    var authorModel = _context.Authors.Where(a => a.Id == item.AuthorId).IgnoreQueryFilters().FirstOrDefault();
                    BookAuthorViewModel bookAuthorViewModel = new BookAuthorViewModel()
                    {
                        books = item,
                        author = authorModel
                    };
                    bookAuthorViewModels.Add(bookAuthorViewModel);
                }

                return bookAuthorViewModels;

            }
            else
            {
                var Books = _context.Books.ToList();
                foreach (var item in Books)
                {
                    var authorModel = _context.Authors.Where(a => a.Id == item.AuthorId).IgnoreQueryFilters().FirstOrDefault();

                    BookAuthorViewModel bookAuthorViewModel = new BookAuthorViewModel()
                    {
                        books = item,
                        author = authorModel

                    };
                    bookAuthorViewModels.Add(bookAuthorViewModel);

                }

                return bookAuthorViewModels;
            }
        }

        public IEnumerable<BookModel> GetBooks()
        {
            var Books = _context.Books.ToList();
            return Books;
        }
        #endregion
        #region save
        public  void Save()
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
        public async Task<BookModel> DeleteBookOnGet(int id)
        {
            var book = await _context.Books
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


                     }).IgnoreQueryFilters().FirstOrDefaultAsync();
            return book;
        }

        public void DeleteBooksOnPost(int id)
        {
            var book =  _context.Books.Find(id);
            _context.Books.Remove(book);
        }
        #endregion
        #region BookById

        public async Task<BookModel> GetBookById(int id)
        {
            BookModel? book = await _context.Books.Where(b => b.Id == id).SingleOrDefaultAsync();
            return book;
        }

        #endregion
    }
}
