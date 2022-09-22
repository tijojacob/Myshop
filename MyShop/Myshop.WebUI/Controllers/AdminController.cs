using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.WebUI.Controllers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute()
        {
            this.Users = (ConfigurationManager.AppSettings["AdminUserId"]).ToString();
        }

        // other stuff
    }
    [CustomAuthorize]

    //[Authorize(Users = "test@gmail.com")] // This applicable to per email id basis
    //[Authorize(Roles = "Admin")] //This can be done by insertig data into
    //AspNetRoles and AspNetUserRoles tables OR
    // using the microsoft claim schemas into AspNetUserClaims table
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}