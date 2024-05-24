using FoodOrderDAL.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.ServiceRepo
{
    public class MailSer : IMailRepo
    {
        public string SendMail(string ToMail)
        {
            string senderMail = "vasanthabalanm.kanini@gmail.com";
            string senderPassword = "jshjmjnbbkdkylwi";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderMail);
            message.Subject = "Resetting the password - regards";
            message.To.Add(new MailAddress(ToMail));

            //template html
            string messageContent = "<html>" +
                "<head>" +
                    "<style>" +
                        "body { font-family: 'Arial', sans-serif; background-color: #f4f4f4;}" +
                        "p { font-weight: 600; color: #333;}" +
                    "</style>" +
                "</head>" +
                "<body>" +
                    "<p>Welcome to Online Food order!</p>" +
                    "<p>Please user your mail and password to Login:</p>"+
                    "<p>Use Email to login :"+ToMail+"</p>"+
                    "<p>Your Password:<b> {tempPassword} </b></p>" +
                "</body>" +
            "</html>";

            string tempPassword = GenerateRandomAlphanumericPassword(10);


            messageContent = messageContent.Replace("{tempPassword}", tempPassword);

            message.Body = messageContent;
            message.IsBodyHtml = true;

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderMail, senderPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
            return tempPassword;
        }

        private static string GenerateRandomAlphanumericPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }

        //approve mail
        public void SendOrderApproveMail(string ToMail)
        {
            string senderMail = "vasanthabalanm.kanini@gmail.com";
            string senderPassword = "jshjmjnbbkdkylwi";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderMail);
            message.Subject = "OrderApproval regard - regards";
            message.To.Add(new MailAddress(ToMail));

            //template html
            string messageContent = "<html>" +
                "<head>" +
                    "<style>" +
                        "body { font-family: 'Arial', sans-serif; background-color: #f4f4f4;}" +
                        "p { font-weight: 600; color: #333;}" +
                    "</style>" +
                "</head>" +
                "<body>" +
                    "<p>Welcome to Online Food order!</p>" +
                    "<p>Please pick your order within 30 mins.</p>"+
                "</body>" +
            "</html>";

            message.Body = messageContent;
            message.IsBodyHtml = true;

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderMail, senderPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}
