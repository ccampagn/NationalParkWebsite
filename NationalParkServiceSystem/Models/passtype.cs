using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class passtype
    {
         public passtype(int passid,string passname )
    {
        this.passid = passid;
        this.passname = passname;
       
    }
        public int getpassid()
        {
            return passid;
        }
        public string getpassname()
        {
            return passname;
        }
        public void setpassid()
        {
            this.passid = passid;
        }
        public void setpassname()
        {
            this.passname = passname;
        }
        private int passid { get; set; }
        private string passname { get; set; }
    }
}