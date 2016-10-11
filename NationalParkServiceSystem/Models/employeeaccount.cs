using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class employeeaccount
    {
         public employeeaccount(password password, address address, parks parks)
        {
            this.password = password;
            this.address = address;
            this.parks = parks;

        }
         private password password { get; set; }
         private address address { get; set; }
         private parks parks { get; set; }
         public void setpassword(password password)
         {
             this.password = password;
         }
         public void setaddress(address address)
         {
             this.address = address;
         }
         public void setparks(parks parks)
         {
             this.parks = parks;
         }
         public password getpassword()
         {
             return password;
         }
         public address getaddress()
         {
             return address;
         }
         public parks getparks()
         {
             return parks;
         }
        
    }
}