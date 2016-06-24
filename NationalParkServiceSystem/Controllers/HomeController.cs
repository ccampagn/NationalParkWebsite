using NationalParkServiceSystem.Models;
using Root.Reports;
using sharpPDF;
using sharpPDF.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationalParkServiceSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


           // createpdf pdf = new createpdf();
           // pdf.helloworld();
                return View();
            

        }
        public ActionResult validateaccount(int id)
        {
            db db = new db();
            bool sucess=db.activeuser(id);
            if (sucess)
            {
                Response.Write(@"<script language='javascript'>alert('Account validate is sucessful');</script>");
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Account validate is not sucessful');</script>");
                return View("~/Views/Home/Index.cshtml");
            }
        }
        public ActionResult validatecodeaccount(int id)
        {
            db db = new db();
            string username = db.getusernamecode(id);
            bool sucess = db.activecodeuser(id);
            if (sucess)
            {
                
                Session["forgetpassword"] = username;
                return View("~/Views/Home/ForgetPasswordCode.cshtml");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Invaild Code');</script>");
                return View("~/Views/Home/Index.cshtml");
            }
        }
        public ActionResult forgetpasswordcode()
        {
            string password = Request.Form["password"];
            bool passwords = false;
            db db = new db();
            string username = (string)Session["forgetpassword"];
            int userid = db.getuserid(username);
            password pw = new password(userid, username, password);
            ArrayList ab = db.getpreviouspassword(pw);
            for (int x = 0; x < ab.Count; x++)
            {
                passwords = passwordhash.ValidatePassword(password, ab[x].ToString());
                if (passwords)
                {
                 Response.Write(@"<script language='javascript'>alert('Password previous use.');</script>");
                 return View("~/Views/Home/ForgetPasswordCode.cshtml");
                }
             }
             db.updatepassword(new password(userid,username, passwordhash.CreateHash(password)));
             Response.Write(@"<script language='javascript'>alert('Update password');</script>");
             return View("~/Views/Home/Index.cshtml");
           
        }
        public ActionResult ForgetPassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ForgetPasswordform()
        {
            db db = new db();
            Random rnd = new Random();
            int code = rnd.Next(1, 999999);
            bool valid = db.checkcode(code);
            while (!valid)
            {
                code = rnd.Next(1, 99999999);
                valid = db.checkcode(code);
            }
            password pw =new password(db.getuserid(Request.Form["username"]),Request.Form["username"],code.ToString());
            bool status=pw.forgetpassword(pw);
            if (status)
            {
                Response.Write(@"<script language='javascript'>alert('Email with code have been send');</script>");
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Invalid User Name');</script>");
                return View("~/Views/Home/ForgetPassword.cshtml");
            }

                
        }

       
        public ActionResult CreateAccount()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateAccountform()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            string firstname = Request.Form["firstname"];
            string middlename = Request.Form["middlename"];
            string lastname = Request.Form["lastname"];
            string suffix = Request.Form["suffix"];
            string address1 = Request.Form["address1"];
            string address2 = Request.Form["address2"];
            string city = Request.Form["city"];
            string state = Request.Form["state"];
            string zipcode = Request.Form["zipcode"];
            string country = Request.Form["country"];
            address address = new address(firstname, middlename, lastname, suffix, address1, address2, city, state, zipcode, country);
            password pw = new password(0,username, passwordhash.CreateHash(password));
            useraccount user = new useraccount(pw, address,0);
            int sucess=pw.CreateAccount(user);
            if (sucess==0)
            {
                Response.Write(@"<script language='javascript'>alert('Account Email was sent in an email');</script>");
                return View("~/Views/Home/Index.cshtml");
            }
            else if (sucess==1)
            {
                Response.Write(@"<script language='javascript'>alert('Username is already in use');</script>");
                return View("~/Views/Home/CreateAccount.cshtml");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Invalid Email Address');</script>");
                return View("~/Views/Home/CreateAccount.cshtml");
            }
        }
        [HttpPost]
        public ActionResult Welcome()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            db db=new db();
            password pw = new password(db.getuserid(username),username, password);
            int sucess = pw.login(pw);
            
            if (sucess==2)
            {
                Session["useraccount"] = new useraccount(pw,db.getaddress(pw.getuserid()),0);
                return View("~/Views/Home/Home.cshtml");
            }
            else if(sucess==0)
            {
                Response.Write(@"<script language='javascript'>alert('Incorrect Username/Password');</script>");
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Incorrect Username/Password');</script>");
                return View("~/Views/Home/Index.cshtml");
            }
        }
        public ActionResult Home()
        {

            if ( Session["useraccount"]!=null)
            {
                return View("~/Views/Home/Home.cshtml");
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

    }
}
