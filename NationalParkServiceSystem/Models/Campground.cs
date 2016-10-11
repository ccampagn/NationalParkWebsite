using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class Campground
    {
        public Campground(int campgroundid, string campgroundname,string description, streetaddress address, GPScoordinate coor,parks park)
        {
            this.campgroundid = campgroundid;
            this.campgroundname = campgroundname;
            this.description = description;
            this.address = address;
            this.coor = coor;
            this.park = park;
        }
        private int campgroundid { get; set; }
        private string campgroundname { get; set; }
        private string description { get; set; }
        private streetaddress address { get; set; }
        private GPScoordinate coor { get; set; }
        private parks park { get; set; }

        public void setcampgroundid(int campgroundid)
        {
            this.campgroundid = campgroundid;
        }
        public int getcampgroundid()
        {
            return campgroundid;
        }
        public void setcampgroundname(string campgroundname)
        {
            this.campgroundname = campgroundname;
        }
        public string getcampgroundname()
        {
            return campgroundname;
        }
        public void setdescription(string description)
        {
            this.description = description;
        }
        public string getdescription()
        {
            return description;
        }
        public void setaddress(streetaddress address)
        {
            this.address = address;
        }
        public GPScoordinate getgpscoordinate()
        {
            return coor;
        }
        public void setgpscoordinate(GPScoordinate coor)
        {
            this.coor = coor;
        }
        public parks getparks()
        {
            return park;
        }
        public void setparks(parks park)
        {
            this.park = park;
        }
        public streetaddress getaddress()
        {
            return address;
        }

        public void addcampground(Campground camp)
        {
           db db = new db();
           int geo=db.addgpscoor(camp.getgpscoordinate());
           int address = db.addaddress(camp.getaddress());
           camp.getaddress().setaddress(address);
           camp.getgpscoordinate().setgpsid(geo);

           db.addcampground(camp);
        }
    }
}