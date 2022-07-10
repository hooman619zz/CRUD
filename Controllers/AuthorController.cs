using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;

namespace CrudTest.Controllers
{
    public class AuthorController : Controller
    {

        #region ctor + jections
        private ApplicationDbContext _context;

        private BookRepository bookRepository;
        private AuthorRepository authorRepository;
        private LibraryRepository libraryRepository;
        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
            bookRepository = new BookRepository(context);
            authorRepository = new AuthorRepository(context);
            libraryRepository = new LibraryRepository(context);
        }

        #endregion

        #region Author


        #region Add Author
        [HttpGet]
        [Authorize(Trust = "yes")]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectResult> AddAuthor(AuthorModel authorModel)
        {
            await Task.Run(()=> authorRepository.InsertAuthorOnPost(authorModel));
            authorRepository.Save();
            return Redirect(@"~/Author/AuthorList");
        }





        #endregion


        #region Delete Author
        [HttpGet]
        [Authorize(Trust = "yes")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {

            return View(await authorRepository.DeleteAuthorOnGet(id));

        }

        [HttpPost]
        public async Task<RedirectResult> DeletAuthorOnPost(int id)
        {
            await Task.Run(()=> authorRepository.DeletAuthorOnPost(id));
            authorRepository.Save();
            return Redirect(@"~/Author/AuthorList");
        }

        #endregion


        #region Author List
        public ActionResult AuthorList()
        {
            return View(authorRepository.AuthorList());
        }
        #endregion


        #endregion

    }
}
