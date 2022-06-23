using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Controllers
{
    public class BookController : Controller
    {
        #region ctor + jections
        private ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion


        #region Insert
        [HttpGet]
        public IActionResult InsertBook()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<RedirectResult> InsertBook(string name, string isbn, string publisher, int authorId, int quantity)
        {
            var author = await _context.Authors.FindAsync(authorId);

            BookModel book = new BookModel()
            {
                Name = name,
                ISBN = isbn,
                Publisher = publisher,
                AuthorModel = author,
                AuthorId = authorId,
                Quantity = quantity
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();


            return Redirect(@"~/Book/ReadBooks");


        }
        #endregion


        #region Read
        public async Task<IActionResult> ReadBooks()
        {

            var Books = await _context.Books.ToListAsync();
            return View(Books);
        }
        #endregion


        #region Delete

        [HttpGet]
        public IActionResult DeleteBooks(int id)
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


                 }).FirstOrDefault();
            return View(book);


        }

        [HttpPost]
        public async Task<RedirectResult> DeleteBooksOnPost(int id)
        {


            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();



            return Redirect(@"~/Book/ReadBooks");
        }

        #endregion


        #region Edit



        [HttpGet]
        public IActionResult OnGet(int id)
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


                 }).FirstOrDefault();
            var authors = _context.Authors.ToList();

            BookListViewModel bookListViewModel = new BookListViewModel()
            {
                BookModel = book,
                AuthorModel = authors
            };

            return View(bookListViewModel);




        }

        [HttpPost]
        public async Task<RedirectResult> OnPost(int id, string name, string isbn, string publisher, int authorId, int quantity)
        {


            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.Name = name;
                book.ISBN = isbn;
                book.Publisher = publisher;
                book.AuthorId = authorId;
                book.Quantity = quantity;
            }

            await _context.SaveChangesAsync();


            return Redirect(@"~/Book/ReadBooks");

        }

        #endregion


        #region Add Author
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectResult> AddAuthor(string name)
        {
            AuthorModel author = new AuthorModel()
            {
                Name = name
            };



            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();


            return Redirect(@"~/Book/AuthorList");
        }





        #endregion


        #region Delete Author
        [HttpGet]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _context.Authors
                .Where(a => a.Id == id)
                 .Select(s => new AuthorModel()
                 {
                     Id = s.Id,
                     Name = s.Name
                 }).FirstOrDefault();
            return View(author);

        }

        [HttpPost]
        public async Task<RedirectResult> DeletAuthorOnPost(int id)
        {


            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();



            return Redirect(@"~/Book/AuthorList");
        }

        #endregion


        #region Author List
        public async Task<IActionResult> AuthorList()
        {

            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }
        #endregion
    }
}
