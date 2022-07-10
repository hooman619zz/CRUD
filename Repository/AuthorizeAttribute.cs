using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;

namespace CrudTest.Repository
{
    public class AuthorizeAttribute : ActionFilterAttribute
    {

        public string Trust { get; set; }



        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string trust = context.HttpContext.Request.Cookies["Token"];

            if (Trust != trust)
            {
                context.Result = new RedirectResult("/Account/LoginOnGet");
            }
        }

    }
}