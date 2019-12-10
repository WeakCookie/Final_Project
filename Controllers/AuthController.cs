using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    public class AuthController : Controller
    {

        Model1Container1 db = new Model1Container1();

        // GET: Auth/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                var currentUser = db.Users.SingleOrDefault(u => (u.Email == user.Email) && (u.Password == user.Password));
                if (currentUser != null)
                {
                    Session["loggedIn"] = currentUser.Email;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.error = "Wrong password and email combination !!!";
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Register
        public ActionResult Register()
        {
            return View();
        }


        // POST: Auth/Register
        [HttpPost]
        public ActionResult Register(User user)
        {
            var existedEmail = db.Users.SingleOrDefault(u => u.Email == user.Email);
            
            if (existedEmail != null)
            {
                ViewBag.error = "The email you registered is not available";
                return View();
            }

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["loggedIn"] = null;
            return RedirectToAction("Index", "Home");
        }
    }

}