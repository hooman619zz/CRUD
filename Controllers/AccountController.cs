using Microsoft.AspNetCore.Mvc;
using CrudTest.Models;
using CrudTest.Data;
using Microsoft.EntityFrameworkCore;
using CrudTest.Repository;

namespace CrudTest.Controllers
{
    public class AccountController : Controller
    {
        #region ctor
        private ApplicationDbContext _context;

        public AccountController(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;

        }
        #endregion


        #region Register
        [HttpGet]
        public IActionResult RegisterOnGet()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult RegisterOnPost(UserModel user)
        {
            string s = user.UserName;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Redirect(@"~/Home/Index");
        }
        #endregion


        #region Login
        public IActionResult LoginOnGet()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult LoginOnPost(UserModel user)
        {
            var users = _context.Users.ToList();
            foreach (var item in users)
            {
                if (user.UserName == item.UserName && user.Password == item.Password)
                {
                    CookieOptions myCookie = new CookieOptions();
                    Response.Cookies.Append("Token", "yes", myCookie);
                    return Redirect(@"~/Home/Index");

                }

            }
            return Redirect(@"~/Account/LoginOnGet");

        }
        #endregion

        public string AccessDenied()
        {
            return "shoma nemitavanid b inja dastresi dashte bashid !";
        }

    }
}
