using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NationalParkServiceSystem.Models
{
    public class streetaddress
    {
          public streetaddress(int strid,string address, string city, string state, string zipcode, string country)
        {
            this.strid = strid;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zipcode = zipcode;
            this.country = country;
        }
          public void setaddress(int strid)
          {
              this.strid = strid;
          }
          public int getaddressid()
          {
              return strid;
          }
        public string getaddress()
        {
            return address;
        }
        public string getcity()
        {
            return city;
        }
        public string getstate()
        {
            return state;
        }
        public string getzipcode()
        {
            return zipcode;
        }
        public string getcountry()
        {
            return country;
        }
        private int strid { get; set; }
        private string address { get; set; }
        private string city { get; set; }
        private string state { get; set; }
        private string zipcode { get; set; }
        private string country { get; set; }
    }
}
