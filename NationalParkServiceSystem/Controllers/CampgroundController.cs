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
    public class CampgroundController : Controller
    {
        //
        // GET: /Campground/

        public ActionResult Index()
        {
            db db = new db();
            List<Campground> directory = db.getallcampground();
            ViewBag.campground = directory;
            return View();
        }

        public ActionResult AddCampground()
        {
            db db = new db();
            return View();
        }

        public ActionResult campgroundinfo(string campgroundid)
        {

            db db = new db();
            int value = Convert.ToInt32(Request["campgroundid"]);
            Campground camp=db.getcampgroundinfo(value);
            Debug.Write(value);
            Debug.Write(camp.getcampgroundid());
            Debug.Write(camp.getcampgroundname());
            Debug.Write("teast");
            ViewBag.campgroundinfo = camp;
            return View();
        }

        [HttpPost]
        public ActionResult addcampgroundform()
        {


            string campground = Request.Form["campground"];
            string description = Request.Form["description"];
            decimal latitude = Convert.ToDecimal(Request.Form["latitude"]);
            decimal longitude = Convert.ToDecimal(Request.Form["longitude"]);
            string address = Request.Form["address"];
            string city = Request.Form["city"];
            string state = Request.Form["state"];
            string zipcode = Request.Form["zipcode"];
            string country = Request.Form["country"];
            int parkid = Convert.ToInt32(Request["parkname"]);
            GPScoordinate geo = new GPScoordinate(0,latitude, longitude);
            streetaddress straddress = new streetaddress(0,address, city, state, zipcode, country);
            parks park = new parks(parkid, null);
            Campground camp = new Campground(0,campground, description, straddress, geo, park);
            camp.addcampground(camp);
            return View();
        }

    }
}
