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

        IUnitOfWork db;
        public AuthorController(ApplicationDbContext context)
        {
            db = new UnitOfWork(context);
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
            await db.AuthorRepository.InsertAuthorOnPost(authorModel);
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
            await db.AuthorRepository.DeletAuthorOnPost(id);
            db.AuthorRepository.Save();
            return Redirect(@"~/Author/AuthorList");
        }

        #endregion


        #region Author List
        [Authorize(Trust = "yes")]
        public async Task<ActionResult> AuthorList()
        {
            return View(await db.AuthorRepository.AuthorList());
        }
        #endregion


        #endregion

    }
}
