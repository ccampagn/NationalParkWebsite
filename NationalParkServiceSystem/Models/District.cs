using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class District
    {
           public District(int districtid,string districtname,int parkid)
    {
        this.districtid =districtid;
        this.parkid = parkid;
        this.districtname = districtname;      
    }
        public int getparkid()
        {
            return parkid;
        }
        public string getdistrictname()
        {
            return districtname;
        }
        public int getdistrictid()
        {
            return districtid;
        }
        public void setdistrictid()
        {
            this.parkid = parkid;
        }
        public void setdistrictname()
        {
            this.districtname = districtname;
        }
        private int districtid { get; set; }
        private string districtname { get; set; }
        private int parkid { get; set; }
    }
    }
