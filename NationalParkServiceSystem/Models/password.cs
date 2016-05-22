using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class password
    {
         public password(int userid,string username,string passwords )
    {
        this.userid = userid;
        this.username = username;
        this.passwords = passwords;
    }
        private int userid { get; set; } 
        private string username { get; set; }
        private string passwords { get; set; }
        public void setuserid(int userid)
        {
            this.userid = userid;
        }
        public void setusername(string username)
        {
            this.username = username;
        }
        public void setpassword(string passwords)
        {
            this.passwords = passwords;
        }
        public int getuserid()
        {
            return userid;
        }
        public string getusername()
        {
            return username;
        }
        public string getpassword()
        {
            return passwords;
        }
        public int CreateAccount(useraccount user)
        {
            int errorcode = 1;
            Random rnd = new Random();
            int code = rnd.Next(1, 999999); 
            db db = new db();
            bool valid=db.checkcode(code);
            while (!valid)
            {
                code = rnd.Next(1, 99999999);
                valid = db.checkcode(code);
            }
            password usercode = new password(0,user.getpassword().getusername(), code.ToString());
            
            bool sucess=db.validnewuser(user.getpassword().getusername());
            if (sucess == true)
            {
                errorcode = 0;
                email email = new email();
                try
                {
                    email.sendnewaccountemail(usercode);
                }
                catch
                {
                    errorcode = 2;
                }
                if (errorcode == 0) {
                    db.newaccount(user, code);                                
             }
            }
            return errorcode;
        }
        public void CreateAccountemployee(employeeaccount password,string username)
        {
            
            db db = new db();
            db.insertaccountemployee(password,username);
            



          
        }
        public int login(password pw)
        {
            db db = new db();
            int id = db.getuserid(pw.getusername());
            bool sucess = false;
            int status = 0;
            if (db.checkdeactive(pw.getusername()))
            {
                status = 4;
            }
            if (id != 0 )
            {
                status = 1;
                if (db.checkiflogin(pw.getusername()))
                {
                    if (db.checkifnotlock(pw.getuserid()))
                    {
                        status = 5;
                        sucess = passwordhash.ValidatePassword(pw.getpassword(), db.getuserhash(pw.getusername()));
                        if (sucess)
                        {
                            status = 2;
                        }
                    }
                }
            }
            
          
            db.insertlogin(id, status);
            return status;
        }
        public bool loginemployee(password pw)
        {
            db db = new db();
            bool sucess = false;
            if (pw.getuserid() != 0)
            {
                sucess = passwordhash.ValidatePassword(pw.getpassword(), db.getemployeehash(pw.getusername()));
                
            }
            db.insertemployeelogin(pw.getuserid(), sucess);
            return sucess;
        }
        public bool updateemployeepassword(password pw,int chargeid)
        {
            db db = new db();
            bool user = db.validemployee(pw.getusername());
            if (user)
            {
                db.updateemployeepassword(pw,chargeid);
            }
            return user;
        }



        public bool forgetpassword(password pw)
        {

            db db = new db();
            email email=new email();
            
           bool forget =db.forgetpassword(pw);
           if (forget)
           {
               email.sendcode(pw);
           }
           return forget;
        }

        public bool checkprevoiusemployee(password pw)
        {
             
            db db = new db();
            ArrayList myAL = db.getemployeepreviouspassword(pw.getusername());
            Debug.Print(pw.getpassword());
            for (int x = 0; x < myAL.Count; x++)
            {
                bool newpassword = passwordhash.ValidatePassword(pw.getpassword(), myAL[x].ToString());
                if (newpassword)
                {
                    return true;
                }
            }
            return false;
        }

        public bool changepassword(password pw, string currentpassword)
        {
            db db = new db();
            bool passwords = false;
            bool checkpassword = passwordhash.ValidatePassword(currentpassword, db.getuserhash(pw.getusername()));
            if (checkpassword)
            {
                ArrayList ab = db.getpreviouspassword(pw);
                for (int x = 0; x < ab.Count; x++)
                {
                    passwords = passwordhash.ValidatePassword(pw.getpassword(), ab[x].ToString());
                    if (passwords)
                    {
                       
                        return false;
                    }
                }
                pw.setpassword(passwordhash.CreateHash(pw.getpassword()));
                db.updatepassword(pw);
                return true;
            }
            return false;
        }
    }

}