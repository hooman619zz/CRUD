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

        public IUserRepository userRepository;
        public AccountController(ApplicationDbContext context)
        {
            this.userRepository = new UserRepository(context);
        }

        #endregion


        #region Register
        [HttpGet]
        public IActionResult RegisterOnGet()
        {
            if (TempData["LoginMessage"] != null)
                ViewBag.Message = TempData["LoginMessage"].ToString();
            return View();
        }

        [HttpPost]
        public RedirectResult RegisterOnPost(UserModel user)
        {
            userRepository.InsertUserOnPost(user);
            userRepository.Save();
            return Redirect(@"~/Home/Index");
        }
        #endregion


        #region Login
        public IActionResult LoginOnGet()
        {
            if (Request.Cookies["Token"] != null)
            {
                Response.Cookies.Delete("Token");
                return Redirect(@"~/Home/Index");
            }
            else
                return View();
        }

        [HttpPost]
        public RedirectResult LoginOnPost(UserModel user)
        {
            var users = userRepository.UserList();
            foreach (var item in users)
            {
                if (user.UserName == item.UserName && user.Password == item.Password)
                {

                    CookieOptions myCookie = new CookieOptions();
                    Response.Cookies.Append("Token", "yes", myCookie);
                    TempData["UserName"] = $"{user.UserName}";
                    return Redirect(@"~/Home/Index");

                }
                else
                    TempData["LoginMessage"] = "Account Peyda nashod ! hamin hala sabte nam konid";


            }
            return Redirect(@"~/Account/RegisterOnGet");

        }


        #endregion


    }
}
