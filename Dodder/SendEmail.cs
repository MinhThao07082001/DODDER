
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Dodder
{
    public class SendEmail
    {
        public bool SendEmailNewpassword(string toEmail, string eSubject, string eContent, int newPassword)
        {
            bool check = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("happyhappylearning2021@gmail.com");
                mail.To.Add(toEmail);
                mail.Subject = eSubject;
                mail.IsBodyHtml = true;
                string content = eContent + newPassword;
                mail.Body = content;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                NetworkCredential networkCredential = new NetworkCredential("happyhappylearning2021@gmail.com", "HappyHappy2021?");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
                check = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return check;
        }
    }
}
