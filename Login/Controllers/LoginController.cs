using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(User modelUser)
        {
            modelUser.LoginErrorMessage = "";
            using (LoginEntities db = new LoginEntities())
            {
                var userDetails = db.Users.Where(x => x.Username == modelUser.Username && x.Password == modelUser.Password).FirstOrDefault();
                if (userDetails != null)
                {
                    Session["userID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    modelUser.LoginErrorMessage = "Wrong Username or Password";
                    return View("Index", modelUser);
                }
            }
        }
        public ActionResult Logout()
        {
            Session["userID"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}