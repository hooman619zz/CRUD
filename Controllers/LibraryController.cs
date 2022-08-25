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

        private ILibraryRepository libraryRepository;
        public LibraryController(ApplicationDbContext context)
        {
            libraryRepository = new LibraryRepository(context);
        }

        #endregion

        #region Library


        #region AddLibrary

        [HttpGet]
        [Authorize(Trust = "yes")]
        public async Task<IActionResult> AddLibrary()
        {
            return View(await libraryRepository.InsertLibraryOnGet());
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

            return View(await libraryRepository.LibraryList());

        }
        #endregion


        #region Delete Library
        [HttpGet]
        [Authorize(Trust = "yes")]
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
        [Authorize(Trust = "yes")]
        public IActionResult UpdateLibrary(int id)
        {
            return View(libraryRepository.UpdateLibraryOnGet(id));
        }

        [HttpPost]
        public RedirectResult UpdateLibraryOnPost(LibraryModel libraryModel, int[] arrays)
        {

            libraryRepository.UpdateLibraryOnPost(libraryModel, arrays);
            libraryRepository.Save();


            return Redirect(@"~/Library/LibraryList");

        }

        #endregion


        #endregion
    }
}
