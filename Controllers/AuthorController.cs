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
        public AuthorController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
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
            await db.AuthorRepository.InsertAsync(authorModel);
            db.AuthorRepository.Save();
            return Redirect(@"~/Author/AuthorList");
        }





        #endregion


        #region Delete Author

        [HttpPost]
        [Authorize(Trust = "yes")]
        public async Task<RedirectResult> DeletAuthorOnPost(int id)
        {
            var author= await db.AuthorRepository.GetByIdAsync(id);
            await db.AuthorRepository.DeleteAsync(author);
            return Redirect(@"~/Author/AuthorList");
        }

        #endregion


        #region Author List
        [Authorize(Trust = "yes")]
        public async Task<ActionResult> AuthorList()
        {
            return View(await db.AuthorRepository.GetAllAsync());
        }
        #endregion


        #endregion

    }
}
