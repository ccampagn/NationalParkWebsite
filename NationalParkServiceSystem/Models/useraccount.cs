using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class useraccount
    {
        public useraccount(password password, address address)
        {
            this.password = password;
            this.address = address;
        }
        private password password { get; set; }
        private address address { get; set; }
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
        public useraccount loadaccount()
        {
            useraccount user = new useraccount(null,null);
            return user;

        }
    }
}