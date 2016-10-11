using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class GPScoordinate
    {
        public GPScoordinate(int gpsid,decimal latitude, decimal longitude)
        {
            this.gpsid = gpsid;
            this.latitude = latitude;
            this.longitude = longitude;
        }
        private int gpsid { get; set; }
        private decimal latitude { get; set; }
        private decimal longitude { get; set; }

        public void setgpsid(int gpsid)
        {
            this.gpsid = gpsid;
        }
        public double getgpsid()
        {
            return gpsid;
        }
        public void setlatitude(decimal latitude)
        {
            this.latitude = latitude;
        }
        public decimal getlatitude()
        {
            return latitude;
        }

        public void setlongitude(decimal longitude)
        {
            this.longitude = longitude;
        }
        public decimal getlongitude()
        {
            return longitude;
        }
    }
}