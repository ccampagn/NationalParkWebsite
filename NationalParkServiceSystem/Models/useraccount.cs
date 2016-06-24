using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class useraccount
    {
        public useraccount(password password, address address,int active)
        {
            this.password = password;
            this.address = address;
            this.active = active;
        }
        private password password { get; set; }
        private address address { get; set; }
        private int active { get; set; }
          public void setpassword(password password)
        {
            this.password = password;
        }
        public void setaddress(address address)
        {
            this.address = address;
        }
        public password getpassword()
        {
            return password;
        }
        public address getaddress()
        {
            return address;
        }
        public int getactive()
        {
            return active;
        }
      
    }
}