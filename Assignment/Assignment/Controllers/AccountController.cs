using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        Crudoperation CrudObj = new Crudoperation();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount user)
        {
            CrudObj.AddUser(user);
            ModelState.Clear();
            Response.Write("Account Succesfully Added");

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            if(ModelState.IsValid)
            {
                var usrr = CrudObj.GetUserInfoForLogin.Single(usr => usr.UserName == user.UserName && usr.Password == user.Password);
                Response.Write("Login Succesful");
            }

            else
            {
                ModelState.AddModelError("", "UserName and Password MisMatch");
            }

            return View();
                 
        }
    }
}