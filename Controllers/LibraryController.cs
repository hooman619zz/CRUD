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

        UnitOfWork db = new UnitOfWork();


        #endregion

        #region Library


        #region AddLibrary

        [HttpGet]
        [Authorize(Trust = "yes")]
        public async Task<IActionResult> AddLibrary()
        {
            return View(await db.LibraryRepository.InsertLibraryOnGet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult AddLibraryOnPost(LibraryModel libraryModel, int[] arrays)
        {

            db.LibraryRepository.InsertLibraryOnPost(libraryModel, arrays);
            db.LibraryRepository.Save();
            return Redirect(@"~/Library/LibraryList");

        }



        #endregion


        #region Library List
        public async Task<IActionResult> LibraryList()
        {

            return View(await db.LibraryRepository.LibraryList());

        }
        #endregion


        #region Delete Library
        [HttpGet]
        [Authorize(Trust = "yes")]
        public IActionResult DeleteLibrary(int id)
        {
            return View(db.LibraryRepository.DeleteLibraryOnGet(id));
        }

        [HttpPost]
        public async Task<RedirectResult> DeleteLibraryOnPost(int id)
        {
            db.LibraryRepository.DeleteLibraryOnPost(id);
            db.LibraryRepository.Save();
            return Redirect(@"~/Library/LibraryList");
        }
        #endregion


        #region Edit Library



        [HttpGet]
        [Authorize(Trust = "yes")]
        public IActionResult UpdateLibrary(int id)
        {
            return View(db.LibraryRepository.UpdateLibraryOnGet(id));
        }

        [HttpPost]
        public RedirectResult UpdateLibraryOnPost(LibraryModel libraryModel, int[] arrays)
        {

            db.LibraryRepository.UpdateLibraryOnPost(libraryModel, arrays);
            db.LibraryRepository.Save();


            return Redirect(@"~/Library/LibraryList");

        }

        #endregion


        #endregion
    }
}
