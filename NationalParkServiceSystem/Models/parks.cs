using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class parks
    {
        public parks(int parkid,string parkname)
    {
        this.parkid = parkid;
        this.parkname = parkname;      
    }
        public int getparkid()
        {
            return parkid;
        }
        public string getparkname()
        {
            return parkname;
        }
        public void setparkid()
        {
            this.parkid = parkid;
        }
        public void setparkname()
        {
            this.parkname = parkname;
        }
        private int parkid { get; set; }
        private string parkname { get; set; }
    }
}