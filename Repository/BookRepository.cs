using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Repository
{
    public class BookRepository :GenericRepository<BookModel>, IBookRepository
    {
        private ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
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

        #endregion

    }
}
