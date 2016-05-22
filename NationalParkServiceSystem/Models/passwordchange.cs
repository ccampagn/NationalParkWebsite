using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class passwordchange
    {
     public passwordchange(password password,string newpassword )
    {
        this.password = password;
        this.newpassword = newpassword;
    }
        private password password { get; set; }
        private string newpassword { get; set; }
        public void setpassword(password password)
        {
            this.password = password;
        }
        public void setnewpassword(string newpassword)
        {
            this.newpassword = newpassword;
        }
        public password getpassword()
        {
            return password;
        }
        public string getnewpassword()
        {
            return newpassword;
        }
       

        
    }
}