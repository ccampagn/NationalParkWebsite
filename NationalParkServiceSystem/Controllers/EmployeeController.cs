using NationalParkServiceSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KeepAutomation.Barcode.Bean;

namespace NationalParkServiceSystem.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccountForm()
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
            password pw = new password(username, passwordhash.CreateHash(password));
            employeeaccount ea = new employeeaccount(pw, address);
            pw.CreateAccountemployee(ea,((employeeaccount)Session["useremployee"]).getpassword().getusername());
            Response.Write(@"<script language='javascript'>alert('Account Creation Sucess user id of " + username + " ');</script>");
            BarCode barcode = new BarCode();
            barcode.Symbology = KeepAutomation.Barcode.Symbology.EAN8;
            barcode.CodeToEncode = "11120223";
            barcode.ChecksumEnabled = true;
            barcode.X = 1;
            barcode.Y = 50;
            barcode.BarCodeWidth = 100;
            barcode.BarCodeHeight = 70;
            barcode.Orientation = KeepAutomation.Barcode.Orientation.Degree90;
            barcode.BarcodeUnit = KeepAutomation.Barcode.BarcodeUnit.Pixel;
            barcode.DPI = 72;
            barcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            barcode.generateBarcodeToImageFile("C:/barcode/new.jpeg");
        
           return View("~/Views/Employee/Welcome.cshtml");
            
        }
         [HttpPost]
        public ActionResult Welcome()
        {
            db db = new db();
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            address address = db.getemployeeaddress(username); 
            password pw = new password(username, password);
            bool sucess = pw.loginemployee(pw);
            if (sucess)
            {
                
                bool changepassword=db.checkifpassword(pw.getusername());
                if (changepassword)
                {
                    Session["changepassword"] = pw.getusername();
                    return View("~/Views/Employee/changepassword.cshtml");
                }
                Session["useremployee"] = new employeeaccount(pw, address);
                   // pw.getusername();
                return View("~/Views/Employee/Welcome.cshtml");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Incorrect Username/Password');</script>");
                return View("~/Views/Employee/Index.cshtml");
            }
        }
         [HttpPost]
         public ActionResult ChangePasswordForm()
         {
             bool newpassword = false;
             string username = (string)Session["changepassword"];
             string password = Request.Form["password"];
             db db = new db();
             password pw = new password(username, password);
             newpassword=pw.checkprevoiusemployee(pw);
             if (newpassword)
             {
                 Response.Write(@"<script language='javascript'>alert('Password Previous Use');</script>");
                 return View("~/Views/Employee/changepassword.cshtml");
             }
             string hashcode = passwordhash.CreateHash(pw.getpassword());
             
             password pws = new password(pw.getusername(), hashcode);
             db.updateemployeepassword(pws);
             db.updatelogin(1,pw.getusername());



             Response.Write(@"<script language='javascript'>alert('New Password Sucessful');</script>");
             return View("~/Views/Employee/Welcome.cshtml");
         }
        public ActionResult CreateAccount()
        {

            db db = new db();
            int id = db.getnewemployeeid(); 
            if (Session["useremployee"] == null)
            {


                
               return View("~/Views/Employee/Index.cshtml");
            }
            
            
            ViewBag.id = id.ToString().PadLeft(8, '0');
            Debug.Print(id.ToString());
            return View();
        }
        public ActionResult UpdateAccount()
        {
            if (Session["useremployee"] == null)
            {
                return View("~/Views/Employee/Index.cshtml");
            }
            return View();
        }
         [HttpPost]
        public ActionResult UpdateAccountForm()
        {
           db db = new db();
             string username = Request.Form["username"];
            string password = Request.Form["password"];
            int id = db.getemployeeid(((employeeaccount)Session["useremployee"]).getpassword().getusername());
            password pw = new password(username, passwordhash.CreateHash(password));
            bool pass = pw.updateemployeepassword(pw,id );
           if (pass)
           {
               if (db.getemployeeid(pw.getusername()) != id)
               {
                   db.updatelogin(0, pw.getusername());
               }
               Response.Write(@"<script language='javascript'>alert('Password Changed');</script>");
               return View("~/Views/Employee/Index.cshtml");
           }
           Response.Write(@"<script language='javascript'>alert('Incorrect Username');</script>");
           return View("~/Views/Employee/Index.cshtml");
        }

    }
}
