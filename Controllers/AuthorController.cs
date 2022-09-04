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

        UnitOfWork db = new UnitOfWork();

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
            await Task.Run(()=> db.AuthorRepository.InsertAuthorOnPost(authorModel));
            db.AuthorRepository.Save();
            return Redirect(@"~/Author/AuthorList");
        }





        #endregion


        #region Delete Author
        //[HttpGet]
        //[Authorize(Trust = "yes")]
        //public async Task<IActionResult> DeleteAuthor(int id)
        //{

        //    return View(await authorRepository.DeleteAuthorOnGet(id));

        //}

        [HttpPost]
        [Authorize(Trust = "yes")]
        public async Task<RedirectResult> DeletAuthorOnPost(int id)
        {
            await Task.Run(()=> db.AuthorRepository.DeletAuthorOnPost(id));
            db.AuthorRepository.Save();
            return Redirect(@"~/Author/AuthorList");
        }

        #endregion


        #region Author List
        [Authorize(Trust = "yes")]
        public ActionResult AuthorList()
        {
            return View(db.AuthorRepository.AuthorList());
        }
        #endregion


        #endregion

    }
}
