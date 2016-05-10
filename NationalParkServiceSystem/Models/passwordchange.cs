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
        public bool changepassword(passwordchange pw)
        {
            db db = new db();
            string hash=db.getuserhash(pw.getpassword().getusername());
            bool password = false;
            bool valid = passwordhash.ValidatePassword(pw.getpassword().getpassword(), hash);
            if (valid)
            {
                ArrayList ab = db.getpreviouspassword(pw.getpassword().getusername());
                for (int x = 0; x < ab.Count; x++)
                {
                    password = passwordhash.ValidatePassword(pw.getnewpassword(), ab[x].ToString());
                    if (password)
                    {
                        return false;
                    }
                }
                pw.setnewpassword(passwordhash.CreateHash(pw.getnewpassword()));
                db.updatepassword(pw);
            }
            return valid;
        }

        
    }
}