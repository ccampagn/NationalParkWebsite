using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class passes
    {
    

         public passes(int passid,passtype passtype,parks parkname,string title, string description,double cost,int time,int buyerable, int visable )
    {
        this.passid = passid;
        this.passtype = passtype;
        this.parkname = parkname;
        this.title = title;
        this.description = description;
        this.cost = cost;
        this.time = time;
        this.buyerable = buyerable;
        this.visable = visable;
    }
         public int getpassid()
         {
             return passid;
         } 
        public passtype getpasstype()
         {
             return passtype;
         }  
        public parks getparkname()
         {
             return parkname;
         } 
        public string gettitle()
         {
             return title;
         }
        public string getdescription()
        {
            return description;
        }
        public double getcost()
        {
            return cost;
        }
        public int gettime()
        {
            return time;
        }
        public int getbuyerable()
        {
            return buyerable;
        }
        public int getvisable()
        {
            return visable;
        }
        public void setpassid(int passid)
        {
            this.passid = passid;
        }
        public void setpasstype(passtype passtype)
        {
            this.passtype = passtype;
        }
        public void setparkname(parks parkname)
        {
            this.parkname = parkname;
        }
        public void settitle(string title)
        {
            this.title = title;
        }
        public void setdescription(string description)
        {
            this.description = description;
        }
        public void setcost(double cost)
        {
            this.cost = cost;
        }
        public void settime(int time)
        {
            this.time = time;
        }
        public void setbuyerable(int buyerable)
        {
            this.buyerable = buyerable;
        }
        public void setvisable(int visable)
        {
            this.visable = visable;
        }
        private int passid { get; set; }
        private passtype passtype { get; set; }
        private parks parkname { get; set; }
        private string title { get; set; }
        private string description { get; set; }
        private double cost { get; set; }
        private int time { get; set; }
        private int buyerable { get; set; }
        private int visable { get; set; }
    }
}