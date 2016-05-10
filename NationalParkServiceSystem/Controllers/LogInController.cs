using NationalParkServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationalParkServiceSystem.Controllers
{
    public class LogInController : Controller
    {
        public ActionResult billingaddress()
        {
            if (Session["useraccount"] == null)
            {
               
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                db db=new db();
                string username = ((useraccount)Session["useraccount"]).getpassword().getusername();
               ViewBag.address=db.getaddress(username);
                return View();
            }
        }
        [HttpPost]
        public ActionResult billingaddressform()
        {
            db db = new db();
            address address = new address(Request.Form["firstname"], Request.Form["middlename"], Request.Form["lastname"], Request.Form["suffix"], Request.Form["address1"], Request.Form["address2"], Request.Form["city"], Request.Form["state"], Request.Form["zipcode"], Request.Form["country"]);
            address.newaddress(address,db.getuserid(((useraccount)Session["useraccount"]).getpassword().getusername()));
            return View("~/Views/Home/Index.cshtml");
        }
      
        public ActionResult changepassword()
        {
            if (Session["useraccount"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
               
                return View();
            }
        }
        [HttpPost]
        public ActionResult changepasswordform()
        {
            string currentpassword = Request.Form["currentpassword"];
            string password = Request.Form["password"];
            string username = ((useraccount)Session["useraccount"]).getpassword().getusername();
            password pw = new password(username, currentpassword);
            passwordchange pwchange = new passwordchange(pw, password);
            bool change = pwchange.changepassword(pwchange);

            //get current hash value
            if (change)
            {
                Response.Write(@"<script language='javascript'>alert('Password Change Sucess');</script>");
                return View("~/Views/Home/Home.cshtml");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Password Change Failed');</script>");
                return View("~/Views/LogIn/ChangePassword.cshtml");
            }
        }

       
        
    }
}
