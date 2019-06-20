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
            string result=CrudObj.AddUser(user);
            ModelState.Clear();

            Response.Write(result);

            if(result.Equals("1"))
            {                
                Response.Write("User Added Successfully");
            }

            else if(result.Equals("-1"))
            {
                Response.Write("User With Similar Username Exists");
            }

            else
            {
                Response.Write("User With Similar EmailId Exists");
            }
            
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount ur)
        {
            
                try
                {
                    UserAccount usrr = CrudObj.GetUserInfoForLogin.Single(usr => usr.UserName == ur.UserName.ToString() && usr.Password == ur.Password.ToString());

                    if (usrr != null)
                    {
                        Session["FirstName"] = usrr.UserName.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                }

                catch (Exception er)
                {
                    if(er.Message.ToString() == "Sequence contains no matching element")
                      {
                    Response.Write("The username and the password not found");
                      }

                    else
                {
                    Response.Write(er.Message);
                }
                }
            

            return View();
                 
        }

        public ActionResult LoggedIn()
        {
            if(Session["FirstName"]!=null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Login");
            }
            
        }
    }
}