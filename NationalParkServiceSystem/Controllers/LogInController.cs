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
                int userid = ((useraccount)Session["useraccount"]).getpassword().getuserid();
               ViewBag.address=db.getaddress(userid);
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
            int userid = Convert.ToInt32(((useraccount)Session["useraccount"]).getpassword().getuserid());
            password pw = new password(userid,username, password);
            bool change = pw.changepassword(pw, currentpassword);
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
