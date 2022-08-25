using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;


namespace CrudTest.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        #region ctor
        private ApplicationDbContext _context;

        public LibraryRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }
        #endregion
        #region Save
        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion
        #region Add Library
        public async Task<List<BookModel>> InsertLibraryOnGet()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }
        public void InsertLibraryOnPost(LibraryModel libraryModel, int[] arrays)
        {
            List<BookModel> books = new List<BookModel>();
            for (int i = 0; i < arrays.Count(); i++)
            {
                BookModel book = _context.Books.Where(b => b.Id == arrays[i]).FirstOrDefault();
                books.Add(book);
            }

            libraryModel.BookModels = books;
            _context.Libraries.Add(libraryModel);


        }



        #endregion
        #region Library List
        public async Task<List<LibraryModel>> LibraryList()
        {
            var libraries = await _context.Libraries.ToListAsync();
            return libraries;
        }
        #endregion
        #region Delete Library
        public LibraryModel DeleteLibraryOnGet(int id)
        {
            var library = _context.Libraries
                            .Where(a => a.Id == id)
                             .Select(s => new LibraryModel()
                             {
                                 Id = s.Id,
                                 Name = s.Name
                             }).FirstOrDefault();
            return library;
        }

        public void DeleteLibraryOnPost(int id)
        {
            var library = _context.Libraries.Find(id);
            _context.Libraries.Remove(library);
        }
        #endregion
        #region Update(Edit) Library
        public LibraryBooksViewModel UpdateLibraryOnGet(int id)
        {
            var library = _context.Libraries
                        .Where(l => l.Id == id)
                         .Select(s => new LibraryModel()
                         {
                             Id = s.Id,
                             Name = s.Name,
                             Address = s.Address


                         }).FirstOrDefault();
            var books = _context.Books.ToList();
            var libraryBooks = _context.Libraries.Include(b => b.BookModels).Where(l => l.Id == id)
                  .Select(s => s.BookModels).SingleOrDefault();
 
            foreach (var item in libraryBooks)
            {
                while (books.Exists(s => s.Id == item.Id))
                {
                    books.Remove(item);
                }
            }


            LibraryBooksViewModel libraryBooksViewModel = new LibraryBooksViewModel();
            libraryBooksViewModel.Library = library;
            libraryBooksViewModel.Books = books;
            return libraryBooksViewModel;

        }

        public void UpdateLibraryOnPost(LibraryModel libraryModel, int[] arrays)
        {
            List<BookModel> books = new List<BookModel>();
            for (int i = 0; i < arrays.Count(); i++)
            {
                BookModel book = _context.Books.Where(b => b.Id == arrays[i]).FirstOrDefault();
                books.Add(book);
            }

            var library = _context.Libraries.Find(libraryModel.Id);
            if (library != null)
            {
                library.Name = libraryModel.Name;
                library.Address = libraryModel.Address;
                library.BookModels = books;
            }

        }
        #endregion

    }
}
