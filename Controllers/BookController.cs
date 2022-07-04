using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;

namespace CrudTest.Controllers
{
    public class BookController : Controller
    {
        #region ctor + jections
        private ApplicationDbContext _context;

        private BookRepository bookRepository;
        private AuthorRepository authorRepository;
        private LibraryRepository libraryRepository;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
            bookRepository = new BookRepository(context);
            authorRepository = new AuthorRepository(context);
            libraryRepository = new LibraryRepository(context);
        }

        #endregion


        #region Book


        #region Insert
        [HttpGet]
        public IActionResult InsertBook()
        {
            return View(bookRepository.InsertBookOnGet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public RedirectResult InsertBook(BookModel bookModel)
        {

            bookRepository.InsertBookOnPost(bookModel);
            bookRepository.Save();


            return Redirect(@"~/Book/ReadBooks");


        }
        #endregion


        #region Read
        public IActionResult ReadBooks(int? id)
        {

            var books = bookRepository.ReadBooks(id);

            return View(books);
        }
        #endregion


        #region Delete

        [HttpGet]
        public IActionResult DeleteBooks(int id)
        {
            return View(bookRepository.DeleteBookOnGet(id));
        }

        [HttpPost]
        public RedirectResult DeleteBooksOnPost(int id)
        {
            bookRepository.DeleteBooksOnPost(id);
            bookRepository.Save();
            return Redirect(@"~/Book/ReadBooks");
        }

        #endregion


        #region Edit



        [HttpGet]
        public IActionResult OnGet(int id)
        {
            return View(bookRepository.UpdateBookOnGet(id));
        }

        [HttpPost]
        public RedirectResult OnPost(BookModel bookModel)
        {


            if (bookModel != null)
            {
                bookRepository.UpdateBookOnPost(bookModel);
            }

            bookRepository.Save();


            return Redirect(@"~/Book/ReadBooks");

        }

        #endregion


        #endregion


        #region Author


        #region Add Author
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult AddAuthor(AuthorModel authorModel)
        {
            authorRepository.InsertAuthorOnPost(authorModel);
            authorRepository.Save();
            return Redirect(@"~/Book/AuthorList");
        }





        #endregion


        #region Delete Author
        [HttpGet]
        public IActionResult DeleteAuthor(int id)
        {

            return View(authorRepository.DeleteAuthorOnGet(id));

        }

        [HttpPost]
        public RedirectResult DeletAuthorOnPost(int id)
        {
            authorRepository.DeletAuthorOnPost(id);
            authorRepository.Save();
            return Redirect(@"~/Book/AuthorList");
        }

        #endregion


        #region Author List
        public ActionResult AuthorList()
        {
            return View(authorRepository.AuthorList());
        }
        #endregion


        #endregion


        #region Library


        #region AddLibrary

        [HttpGet]
        public IActionResult AddLibrary()
        {
            return View(libraryRepository.InsertLibraryOnGet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult AddLibraryOnPost(LibraryModel libraryModel, int[] arrays)
        {

            libraryRepository.InsertLibraryOnPost(libraryModel, arrays);
            libraryRepository.Save();
            return Redirect(@"~/Book/LibraryList");

        }



        #endregion


        #region Library List
        public async Task<IActionResult> LibraryList()
        {

            return View(libraryRepository.LibraryList());

        }
        #endregion


        #region Delete Library
        [HttpGet]
        public IActionResult DeleteLibrary(int id)
        {
            return View(libraryRepository.DeleteLibraryOnGet(id));
        }

        [HttpPost]
        public async Task<RedirectResult> DeleteLibraryOnPost(int id)
        {
            libraryRepository.DeleteLibraryOnPost(id);
            libraryRepository.Save();
            return Redirect(@"~/Book/LibraryList");
        }
        #endregion


        #region Edit Library



        [HttpGet]
        public IActionResult UpdateLibrary(int id)
        {
            return View(libraryRepository.UpdateLibraryOnGet(id));
        }

        [HttpPost]
        public RedirectResult UpdateLibraryOnPost(LibraryModel libraryModel)
        {

            libraryRepository.UpdateLibraryOnPost(libraryModel);
            libraryRepository.Save();


            return Redirect(@"~/Book/LibraryList");

        }

        #endregion


        #endregion

    }
}
