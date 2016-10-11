using NationalParkServiceSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationalParkServiceSystem.Controllers
{
    public class PassesController : Controller
    {
        //
        // GET: /Passes/

        public ActionResult Index()
        {
           
            db db = new db();
            ArrayList a=db.getpasses(0,0);
            ArrayList park = db.getparknameall();
            ArrayList passtype = db.getpasstypeall();
            ViewBag.password = (useraccount)Session["useraccount"];
            ViewBag.park = park;
            ViewBag.pass = passtype;
            ViewBag.name = a;
            return View();
        }
        public ActionResult search(string parknum,string passtype)
        {

            db db = new db();
            int value = Convert.ToInt32(Request["parkname"]);
            int values = Convert.ToInt32(Request["passtype"]);
            ArrayList a = db.getpasses(value,values);
            ArrayList park = db.getparknameall();
            ArrayList passtypes = db.getpasstypeall();
            ViewBag.password = (useraccount)Session["useraccount"];
            ViewBag.park = park;
            ViewBag.pass = passtypes;
            ViewBag.name = a;
            return View("~/Views/Passes/Index.cshtml");
        }

        [HttpPost]
        public ActionResult BuyNow()
        {

            string id = (string)Request["passid"];
            db db = new db();
            int addresscode= db.getaddressscode( db.getuserid(((useraccount)Session["useraccount"]).getpassword().getusername()));
            db.insertpasses(Convert.ToInt32(id), ((useraccount)Session["useraccount"]).getpassword().getuserid());
            createpdf a = new createpdf();
            a.createpass(((useraccount)Session["useraccount"]));
            return View();
        }

    }
}
