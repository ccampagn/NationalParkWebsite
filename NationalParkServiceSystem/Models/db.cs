using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class db
    {


        public MySql.Data.MySqlClient.MySqlConnection openconn()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=192.168.1.154;uid=root;" +
                "pwd=newyork;database=nationalpark;";
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            return conn;

        }
        public void closeconn(MySql.Data.MySqlClient.MySqlConnection conn)
        {
            conn.Close();
        }

        public void insertaccount(password password,int code)
        {

            db db = new db();
           MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
           string sql = "INSERT INTO Password (username, password,datecreated) VALUES (@username,@password,Now())";
           MySqlCommand cmd = new MySqlCommand(sql, conn);
           cmd.Parameters.AddWithValue("@username", password.getusername());
           cmd.Parameters.AddWithValue("@password", password.getpassword());
           cmd.ExecuteNonQuery();
            db.closeconn(conn);
            db.insertvalidatecode(password.getusername(),code);
        }

       
        
        

        public void insertvalidatecode(string username,int code)
        {
            db db = new db();
            int id=db.getuserid(username);
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            string sql = "INSERT INTO uservalidate (validationcode, userid,time) VALUES (@randomcode,@id,NOW())";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@randomcode",code);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }

       
        public string getuserhash(String username)
        {
            db db = new db();
            db.cleanouttable();
            int id = db.getuserid(username);
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String hash = "";
            String sql = "SELECT password FROM password where username = @username and active =1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                hash = rdr[0].ToString();
            }
            rdr.Close();
            return hash;
        }


    

        public int getuserid(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            int userid = 0;
            String sql = "SELECT idpassword FROM password where username = @username and active!=2";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                userid = (int)rdr[0];
            }
            rdr.Close();
            db.closeconn(conn);
            return userid;
        }
        public bool validnewuser(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM password where username = @username AND active!=2";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return false;
            }
            rdr.Close();
            db.closeconn(conn);
            return true;
        }



        public bool  checkcode(int code)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM uservalidate where validationcode = @code";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@code", code);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return false;
            }
            rdr.Close();
            db.closeconn(conn);
            return true;
        }
        public bool activeuser(int code)
        {
            int userid = 0;
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT userid FROM uservalidate where validationcode = @code";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@code", code);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                userid = (int)rdr[0];
            }
            rdr.Close();
            if (userid != 0)
            {
                sql = "DELETE FROM uservalidate where userid = @userid";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.ExecuteNonQuery();

            
                sql = "UPDATE password SET active=1 where idpassword = @userid AND active=0";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.ExecuteNonQuery();
                db.closeconn(conn);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool activecodeuser(int code)
        {
            int userid = 0;
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT userid FROM forgetcode where code = @code";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@code", code);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                userid = (int)rdr[0];
            }
            rdr.Close();
            if (userid != 0)
            {
                sql = "DELETE FROM forgetcode where userid = @userid";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.ExecuteNonQuery();
                db.closeconn(conn);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkifactive(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM password where active=1 AND username=@username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return true;
            }
            rdr.Close();
            db.closeconn(conn);
            return false; 
        }
        public int getuseridactive(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            int userid = 0;
            String sql = "SELECT idpassword FROM password where username = @username and active=1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                userid = (int)rdr[0];
            }
            rdr.Close();
            db.closeconn(conn);
            return userid;
        }

        public void insertlogin(int id, int sucess)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            string sql = "INSERT INTO loginlog (userid, sucess,time) VALUES (@userid,@sucess,Now())";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", id);
            cmd.Parameters.AddWithValue("@sucess", sucess);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        public void insertemployeelogin(int id, bool sucess)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            string sql = "INSERT INTO employeelog (employeeid, sucess,time) VALUES (@userid,@sucess,Now())";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", id);
            cmd.Parameters.AddWithValue("@sucess", sucess);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }

        public void insertpassword(password password)
        {
            db db = new db();
                MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
                string sql = "INSERT INTO passwordchangehistory (userid, hashvalue,date) VALUES (@userid,@hashvalue,Now())";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", db.getuserid(password.getusername()));
                cmd.Parameters.AddWithValue("@hashvalue", password.getpassword());
                cmd.ExecuteNonQuery();
                db.closeconn(conn);

        }



      
        public ArrayList getpreviouspassword(password password)
        {
            ArrayList myAL = new ArrayList();
            db db = new db();
           
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT hashvalue FROM passwordchangehistory where userid=@userid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", password.getuserid());
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                myAL.Add(rdr[0].ToString());
            }
            rdr.Close();
            db.closeconn(conn);
            return myAL;
        }
        public ArrayList getpreviousemployeepassword(string username)
        {
            ArrayList myAL = new ArrayList();
            db db = new db();
            int userid = db.getemployeeid(username);
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT hashvalue FROM passwordchangehistory where userid=@userid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                myAL.Add(rdr[0].ToString());
            }
            rdr.Close();
            db.closeconn(conn);
            return myAL;
        }
        
        public void updateemployeepassword(password pw)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "UPDATE employee SET password=@hashvalue where username = @userid";
           
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@hashvalue", pw.getpassword());
            cmd.Parameters.AddWithValue("@userid", pw.getusername());
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
            db.insertemployeepassword(pw);
        }



        public bool forgetpassword(password pw)
        {
            db db = new db();
            if (pw.getuserid() != 0)
            {
                if (db.checkifactive(pw.getusername()))
                {
                    MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
                    string sql = "INSERT INTO forgetcode (code, userid,date) VALUES (@randomcode,@id,NOW())";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@randomcode", pw.getpassword());
                    cmd.Parameters.AddWithValue("@id", pw.getuserid());
                    cmd.ExecuteNonQuery();
                    db.closeconn(conn);
                    return true;
                }
            }
            return false;
        }

        public void updatepassword(password pw)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "UPDATE password SET password=@password where idpassword = @userid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@password", pw.getpassword());
            cmd.Parameters.AddWithValue("@userid", pw.getuserid());
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO passwordchangehistory (userid, hashvalue,date) VALUES (@userid,@hashvalue,Now())";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", pw.getuserid());
            cmd.Parameters.AddWithValue("@hashvalue", pw.getpassword());
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
            db.closeconn(conn);
            
        }

        public string getusernamecode(int id)
        {

            string username = "";
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT username FROM nationalpark.password JOIN nationalpark.forgetcode ON userid=idpassword where forgetcode.code = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
             cmd.Parameters.AddWithValue("@username", id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                username=rdr[0].ToString();
            }
            rdr.Close();
            db.closeconn(conn);
            return username;
        }
        public ArrayList getpasses(int park,int passtype)
        {

            ArrayList a = new ArrayList();
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            
            String sql = "SELECT * FROM nationalpark.passes";
            if (park != 0)
            {
                sql = "SELECT * FROM nationalpark.passes where idpark=@park";
            }
            if (passtype != 0)
            {
                sql = "SELECT * FROM nationalpark.passes where idtype=@pass";
            }
            if (passtype != 0 && park!=0)
            {
                sql = "SELECT * FROM nationalpark.passes where idpark=@park AND idtype=@pass";
            } 
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@park", park);
            cmd.Parameters.AddWithValue("@pass", passtype);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                
                passes pass = new passes(Convert.ToInt32(rdr[0]), db.getpasstype(Convert.ToInt32(rdr[2])), db.getparkname(Convert.ToInt32(rdr[1])), rdr[6].ToString(), rdr[7].ToString(), Convert.ToDouble(rdr[3]), Convert.ToInt32(rdr[8]), Convert.ToInt32(rdr[4]), Convert.ToInt32(rdr[5]));
                a.Add(pass);
            }
            rdr.Close();
            db.closeconn(conn);
            return a;
        }

        public parks getparkname(int parkid)
        {
            parks park=null;
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM park where idpark=@parkid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@parkid", parkid);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                park=new parks(Convert.ToInt32(rdr[0]),rdr[1].ToString());
            }
            rdr.Close();
            db.closeconn(conn);
            return park; 
        }

        public passtype getpasstype(int passtype)
        {
            passtype passtypes = null;
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM passtype where idpasstype=@passtype";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@passtype", passtype);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                passtypes = new passtype(Convert.ToInt32(rdr[0]),rdr[1].ToString());
            }
            rdr.Close();
            db.closeconn(conn);
            return passtypes;

            
        }
        public ArrayList getpasstypeall()
        {
            ArrayList passtypes = new ArrayList();
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM passtype";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                passtypes.Add(new passtype(Convert.ToInt32(rdr[0]), rdr[1].ToString()));
            }
            rdr.Close();
            db.closeconn(conn);
            return passtypes;


        }
        public ArrayList getparknameall()
        {
            ArrayList passtypes = new ArrayList();
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM park";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                passtypes.Add(new parks(Convert.ToInt32(rdr[0]),rdr[1].ToString()));
            }
            rdr.Close();
            db.closeconn(conn);
            return passtypes;


        }
        public void insertpasses(int barcode,int addresscode)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            string sql = "INSERT INTO purchpass (passcode, parkcode,purchasetime,addresscode) VALUES (@passcode,@parkcode,Now(),@addresscode)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@passcode", db.getnewbarcode().ToString().PadLeft(12, '0'));
            cmd.Parameters.AddWithValue("@parkcode", barcode);
            cmd.Parameters.AddWithValue("@addresscode",addresscode);
  
            cmd.ExecuteNonQuery();
            db.closeconn(conn);



        }

        private int getdate(int parknum)
        {
            
            int time = 0;
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT time FROM passes where idpasses=@passcode";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@passcode", parknum);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                time = Convert.ToInt32(rdr[0]);
            }
            rdr.Close();
            db.closeconn(conn);
            
            return time;
        }

        public int getnewbarcode()
        {
            int passid = 0;
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM passbarcode";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                passid =Convert.ToInt32(rdr[1].ToString());
            }
            rdr.Close();
            sql = "UPDATE passbarcode SET passbarcodecol=@newbarcode";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@newbarcode", passid+1);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
            return passid;
        }
        public void insertaccountemployee(employeeaccount password,string username)
        {

            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            string sql = "INSERT INTO Employee (username, password,idcreated,passwordlogin,datecreated) VALUES (@username,@password,@create,0,Now())";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", password.getpassword().getusername());
            cmd.Parameters.AddWithValue("@password", password.getpassword().getpassword());
            cmd.Parameters.AddWithValue("@create", db.getemployeeid(username));
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO employeechangehistory (employeeid, hashvalue,date,changerid) VALUES (@employeeid,@hashvalue,Now(),@chargeid)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@employeeid", db.getemployeeid(password.getpassword().getusername()));
            cmd.Parameters.AddWithValue("@hashvalue", password.getpassword().getpassword());
            cmd.Parameters.AddWithValue("@chargeid", db.getemployeeid(password.getpassword().getusername()));
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO employeeaddress (firstname,middlename,lastname,suffix,address1,address2,city,state,zipcode,country,employeeid) values (@firstname,@middlename,@lastname,@suffix,@address1,@address2,@city,@state,@zipcode,@country,@employeeid)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@employeeid", db.getemployeeid(password.getpassword().getusername()));
            cmd.Parameters.AddWithValue("@firstname", password.getaddress().getfirstname());
            cmd.Parameters.AddWithValue("@middlename", password.getaddress().getmiddlename());
            cmd.Parameters.AddWithValue("@lastname", password.getaddress().getlastname());
            cmd.Parameters.AddWithValue("@suffix", password.getaddress().getsuffix());
            cmd.Parameters.AddWithValue("@address1", password.getaddress().getaddress1());
            cmd.Parameters.AddWithValue("@address2", password.getaddress().getaddress2());
            cmd.Parameters.AddWithValue("@city", password.getaddress().getcity());
            cmd.Parameters.AddWithValue("@state", password.getaddress().getstate());
            cmd.Parameters.AddWithValue("@zipcode", password.getaddress().getzipcode());
            cmd.Parameters.AddWithValue("@country", password.getaddress().getcountry());
            cmd.ExecuteNonQuery();
            db.closeconn(conn);              
        }
        public bool validnewemployee(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM employee where username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return false;
            }
            rdr.Close();
            db.closeconn(conn);
            return true;
        }

        public void insertemployeepassword(password password)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            string sql = "INSERT INTO employeechangehistory (employeeid, hashvalue,date,changerid) VALUES (@employeeid,@hashvalue,Now(),@chargeid)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@employeeid", db.getemployeeid(password.getusername()));
            cmd.Parameters.AddWithValue("@hashvalue", password.getpassword());
            cmd.Parameters.AddWithValue("@chargeid", db.getemployeeid(password.getusername()));
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }

        public int getemployeeid(string p)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            int userid = 0;
            String sql = "SELECT idemployee FROM employee where username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", p);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                userid = (int)rdr[0];
            }
            rdr.Close();
            db.closeconn(conn);
            return userid;
        }

        public string getemployeehash(string p)
        {
            db db = new db();
            int id = db.getuserid(p);
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String hash = "";
            String sql = "SELECT password FROM employee where username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", p);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                hash = rdr[0].ToString();
            }
            rdr.Close();
            return hash;
        }

        public bool validemployee(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM employee where username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return true;
            }
            return false;
        }

        public ArrayList getemployeepreviouspassword(string username)
        {
            ArrayList myAL = new ArrayList();
            db db = new db();
            int userid = db.getemployeeid(username);
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT hashvalue FROM employeechangehistory where employeeid=@userid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                myAL.Add(rdr[0].ToString());
            }
            rdr.Close();
            db.closeconn(conn);
            return myAL;
        }


        public void updateemployeepassword(password pw, int charger)
        {
            db db = new db();
            
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "UPDATE employee SET password=@password where username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", pw.getusername());
            cmd.Parameters.AddWithValue("@password", pw.getpassword());
           cmd.ExecuteNonQuery();
          
           sql = "INSERT INTO employeechangehistory (employeeid, hashvalue,date,changerid) VALUES (@employeeid,@hashvalue,Now(),@changerid)";
           cmd = new MySqlCommand(sql, conn);
           cmd.Parameters.AddWithValue("@employeeid", db.getemployeeid(pw.getusername()));
           cmd.Parameters.AddWithValue("@hashvalue", pw.getpassword());
           cmd.Parameters.AddWithValue("@changerid", charger);
           cmd.ExecuteNonQuery();
           db.closeconn(conn);
            db.closeconn(conn);
                
        }

        public bool checkifpassword(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT passwordlogin FROM employee where username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                if (rdr[0].ToString() == "0")
                {
                    return true;
                }
            }
            return false;
        }

        public void updatelogin(int status,string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "UPDATE employee SET passwordlogin=@status where username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        public address getaddress(int userid)
        {
            db db = new db();
            address address = null;
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM billingaddress where userid = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", userid);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                address = new address(rdr[1].ToString(), rdr[2].ToString(),rdr[3].ToString(), rdr[4].ToString(),rdr[5].ToString(), rdr[6].ToString(),rdr[7].ToString(), rdr[8].ToString(),rdr[9].ToString(),rdr[10].ToString());
            }

            return address;
        }

        public void addnewbillingaddres(address address,int userid)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "INSERT INTO billingaddress (firstname,middlename,lastname,suffix,address1,address2,city,state,zipcode,country,userid) values (@firstname,@middlename,@lastname,@suffix,@address1,@address2,@city,@state,@zipcode,@country,@userid)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@firstname", address.getfirstname());
            cmd.Parameters.AddWithValue("@middlename", address.getmiddlename());
            cmd.Parameters.AddWithValue("@lastname", address.getlastname());
            cmd.Parameters.AddWithValue("@suffix", address.getsuffix());
            cmd.Parameters.AddWithValue("@address1", address.getaddress1());
            cmd.Parameters.AddWithValue("@address2", address.getaddress2());
            cmd.Parameters.AddWithValue("@city", address.getcity());
            cmd.Parameters.AddWithValue("@state", address.getstate());
            cmd.Parameters.AddWithValue("@zipcode", address.getzipcode());
            cmd.Parameters.AddWithValue("@country", address.getcountry());
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
        public int getnewemployeeid()
        {

            int employeeid = 0;
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM employeeid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                employeeid = Convert.ToInt32(rdr[1].ToString());
                
            }
            rdr.Close();
            sql = "UPDATE employeeid SET availableid=@newbarcode";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@newbarcode", employeeid + 1);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
            return employeeid;
        }



        public void updatebillingaddress(int userid)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "UPDATE billingaddress SET userid=0 where userid = @userid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }



        public int getaddressscode(int id)
        {
            db db = new db();
            int addressid = 0;
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM billingaddress where userid=@userid";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                addressid = Convert.ToInt32(rdr[0].ToString());

            }
            rdr.Close();
            db.closeconn(conn);
            return addressid;
        }
        public void cleanouttable()
        {
            db db = new db();
            int userid = 0;
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "DELETE FROM forgetcode WHERE date < (NOW() - INTERVAL 1 HOUR)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            sql = "SELECT userid FROM uservalidate WHERE time < (NOW() - INTERVAL 1 HOUR)";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                userid = Convert.ToInt32(rdr[0].ToString());

            }
            rdr.Close();
            if (userid != 0)
            {
                sql = "DELETE FROM uservalidate WHERE time < (NOW() - INTERVAL 1 HOUR)";
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                rdr.Close();
                sql = "UPDATE password set active = 2 WHERE idpassword=@userid";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                rdr = cmd.ExecuteReader();
                rdr.Close();
            }
            db.closeconn(conn);

        }




        public bool checkdeactive(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM password where active=2 AND username=@username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return true;
            }
            rdr.Close();
            db.closeconn(conn);
            return false; 
        }

        public address getemployeeaddress(string username)
        {
            db db = new db();
            address address = null;
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM employeeaddress where employeeid = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", db.getemployeeid(username));
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                address = new address(rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString(), rdr[5].ToString(), rdr[6].ToString(), rdr[7].ToString(), rdr[8].ToString(), rdr[9].ToString(), rdr[10].ToString());
            }

            return address;
        }

        public List<useraccount> getalluser()
        {
            db db = new db();
            List<useraccount> account = new List<useraccount>();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT username,firstname,middlename,lastname,suffix,address1,address2,city,state,zipcode,country,idpassword,locks FROM nationalpark.password JOIN nationalpark.billingaddress ON userid=idpassword where active!=2";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                password password = new password(Convert.ToInt32(rdr[11]),rdr[0].ToString(), null);
                address address = new address(rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString(), rdr[5].ToString(), rdr[6].ToString(), rdr[7].ToString(), rdr[8].ToString(), rdr[9].ToString(), rdr[10].ToString());
                useraccount user = new useraccount(password, address, Convert.ToInt32(rdr[12].ToString()));
                account.Add(user);

            }
            rdr.Close();
            db.closeconn(conn);
            return account;
        }

        public bool checkiflogin(string username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM password where active=1 AND username=@username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return true;
            }
            rdr.Close();
            db.closeconn(conn);
            return false; 
        }

        public void newaccount(useraccount user, int code)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            string sql = "INSERT INTO Password (username, password,datecreated) VALUES (@username,@password,Now())";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", user.getpassword().getusername());
            cmd.Parameters.AddWithValue("@password", user.getpassword().getpassword());
            cmd.ExecuteNonQuery();
            int userid = db.getuserid(user.getpassword().getusername());
            sql = "INSERT INTO uservalidate (validationcode, userid,time) VALUES (@randomcode,@id,NOW())";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@randomcode", code);
            cmd.Parameters.AddWithValue("@id", userid);
            cmd.ExecuteNonQuery();           
            sql = "INSERT INTO passwordchangehistory (userid, hashvalue,date) VALUES (@userid,@hashvalue,Now())";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@hashvalue", user.getpassword().getpassword());
            cmd.ExecuteNonQuery();
            sql = "INSERT INTO billingaddress (firstname,middlename,lastname,suffix,address1,address2,city,state,zipcode,country,userid) values (@firstname,@middlename,@lastname,@suffix,@address1,@address2,@city,@state,@zipcode,@country,@userid)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@firstname", user.getaddress().getfirstname());
            cmd.Parameters.AddWithValue("@middlename", user.getaddress().getmiddlename());
            cmd.Parameters.AddWithValue("@lastname", user.getaddress().getlastname());
            cmd.Parameters.AddWithValue("@suffix", user.getaddress().getsuffix());
            cmd.Parameters.AddWithValue("@address1", user.getaddress().getaddress1());
            cmd.Parameters.AddWithValue("@address2", user.getaddress().getaddress2());
            cmd.Parameters.AddWithValue("@city", user.getaddress().getcity());
            cmd.Parameters.AddWithValue("@state", user.getaddress().getstate());
            cmd.Parameters.AddWithValue("@zipcode", user.getaddress().getzipcode());
            cmd.Parameters.AddWithValue("@country", user.getaddress().getcountry());
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
          
            
        }

        public bool checkifnotlock(int username)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "SELECT * FROM password where locks=0 AND idpassword=@username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return true;
            }
            rdr.Close();
            db.closeconn(conn);
            return false;
        }

        public void lockaccount(int locks,int userid)
        {
            db db = new db();
            MySql.Data.MySqlClient.MySqlConnection conn = db.openconn();
            String sql = "UPDATE password SET locks=@locks where idpassword = @userid";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@locks", locks);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);
        }
    }
}