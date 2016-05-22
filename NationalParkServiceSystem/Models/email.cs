using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;


namespace NationalParkServiceSystem.Models
{
    public class email
    {
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void sendnewaccountemail(password password){
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("ccampagn@ycp.edu");
            mail.To.Add(password.getusername());
            
            mail.Subject = "Welcome to the National Park System Website";
            mail.Body = "Your account need to be validate at the link http://localhost:61940/Home/validateaccount/" + password.getpassword()+". The code will expire in 1 hour.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ccampagn@ycp.edu", "newyork13");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
    }

        public void sendcode(password password)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("ccampagn@ycp.edu");
            mail.To.Add(password.getusername());
            mail.Subject = "New Password";
            mail.Body = "The new password need to be enter at http://localhost:61940/Home/validatecodeaccount" + password.getpassword();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ccampagn@ycp.edu", "newyork13");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}