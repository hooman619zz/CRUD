using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;


namespace CrudTest.Repository
{
    public class LibraryRepository :GenericRepository<LibraryModel>, ILibraryRepository
    {
        #region ctor
        private ApplicationDbContext _context;

        public LibraryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
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
            var books = await _context.Set<BookModel>().ToListAsync();
            return books;
        }
        public async Task InsertLibraryOnPost(LibraryModel libraryModel, int[] arrays)
        {
            List<BookModel> books = new List<BookModel>();
            for (int i = 0; i < arrays.Count(); i++)
            {
                BookModel book = await _context.Set<BookModel>().Where(b => b.Id == arrays[i]).FirstOrDefaultAsync();
                books.Add(book);
            }

            libraryModel.BookModels = books;
            await _context.Set<LibraryModel>().AddAsync(libraryModel);


        }



        #endregion
        #region Delete Library
        public LibraryModel DeleteLibraryOnGet(int id)
        {
            var library = _context.Set<LibraryModel>()
                            .Where(a => a.Id == id)
                             .Select(s => new LibraryModel()
                             {
                                 Id = s.Id,
                                 Name = s.Name
                             }).FirstOrDefault();
            return library;
        }


        #endregion
        #region Update(Edit) Library
        public LibraryBooksViewModel UpdateLibraryOnGet(int id)
        {
            var library = _context.Set<LibraryModel>()
                        .Where(l => l.Id == id)
                         .Select(s => new LibraryModel()
                         {
                             Id = s.Id,
                             Name = s.Name,
                             Address = s.Address


                         }).FirstOrDefault();
            var books = _context.Set<BookModel>().ToList();
            var libraryBooks = _context.Set<LibraryModel>().Include(b => b.BookModels).Where(l => l.Id == id)
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
                BookModel book = _context.Set<BookModel>().Where(b => b.Id == arrays[i]).FirstOrDefault();
                books.Add(book);
            }

            var library = _context.Set<LibraryModel>().Find(libraryModel.Id);
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
