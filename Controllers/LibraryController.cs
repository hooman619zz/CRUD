using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;

namespace CrudTest.Controllers
{
    public class LibraryController : Controller
    {
        #region ctor + jections
        private ApplicationDbContext _context;

        private BookRepository bookRepository;
        private AuthorRepository authorRepository;
        private LibraryRepository libraryRepository;
        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
            bookRepository = new BookRepository(context);
            authorRepository = new AuthorRepository(context);
            libraryRepository = new LibraryRepository(context);
        }

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
            return Redirect(@"~/Library/LibraryList");

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
            return Redirect(@"~/Library/LibraryList");
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


            return Redirect(@"~/Library/LibraryList");

        }

        #endregion


        #endregion
    }
}
