using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class address
    {
        public address(string firstname, string middlename, string lastname, string suffix, string address1, string address2, string city, string state, string zipcode, string country)
        {
            this.firstname = firstname;
            this.middlename = middlename;
            this.lastname = lastname;
            this.suffix = suffix;
            this.address1 = address1;
            this.address2 = address2;
            this.city = city;
            this.state = state;
            this.zipcode = zipcode;
            this.country = country;
        }
        public string getfirstname()
        {
            return firstname;
        }
        public string getmiddlename()
        {
            return middlename;
        }
        public string getlastname()
        {
            return lastname;
        }
        public string getsuffix()
        {
            return suffix;
        }
        public string getaddress1()
        {
            return address1;
        }
        public string getaddress2()
        {
            return address2;
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
        private string firstname { get; set; }
        private string middlename { get; set; }
        private string lastname { get; set; }
        private string suffix { get; set; }
        private string address1 { get; set; }
        private string address2 { get; set; }
        private string city { get; set; }
        private string state { get; set; }
        private string zipcode { get; set; }
        private string country { get; set; }

        public void newaddress(address address,int userid)
        {
            db db = new db();
            db.updatebillingaddress(userid);
            db.addnewbillingaddres(address, userid);
            
        }
    }
}