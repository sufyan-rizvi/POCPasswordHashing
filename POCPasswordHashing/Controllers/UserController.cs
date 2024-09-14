using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POCPasswordHashing.Data;
using POCPasswordHashing.Models;
using BC =BCrypt.Net;

namespace POCPasswordHashing.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                using (var s = Helper.CreateSession())
                {
                    var existingUser = s.Query<User>().FirstOrDefault(u => u.Name.ToLower() == user.Name.ToLower());
                    if (existingUser != null)
                    {
                        if (existingUser.Name.ToLower() == user.Name.ToLower() && BC.BCrypt.EnhancedVerify(user.Password, existingUser.Password))
                        {
                            return RedirectToAction("Success");
                        }
                    }
                    ViewData["LoginFailed"] = "Invalid Username or Password";
                    return View();

                }
            }
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (var s = Helper.CreateSession())
                {
                    using (var txn = s.BeginTransaction())
                    {
                        user.Password = BC.BCrypt.EnhancedHashPassword(user.Password,6);
                        s.Save(user);
                        txn.Commit();
                        return RedirectToAction("index");
                    }
                }
            }
            return View();
        }
    }
}