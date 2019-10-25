using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using Gas_Go_v1.Models;
using System.Net;
using System.Web.Services.Description;

namespace Gas_Go_v1.Services
{
    public class EmailSender
    {
        public void Send(String toEmail, String subject, String contents, String fileAddress, String fileName)
        {
            String API_KEY = System.Web.Configuration.WebConfigurationManager.AppSettings["SendGridAPIKey"];
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@gas_go.com", "Gas Go");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p >";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            if (fileAddress != null && fileName != null)
            {
                var bytes = File.ReadAllBytes(fileAddress);
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(fileName, file);
            }
            var response = client.SendEmailAsync(msg);
        }

        public void SendWithTemplate(String toEmail, String fromEmail, String subject, String body)
        {
            SmtpClient client = new SmtpClient("smtp.sendgrid.net");
            String SendGridUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SendGridUserName"];
            String SendGridUserPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SendGridUserPassword"];
            client.Credentials = new NetworkCredential(SendGridUserName, SendGridUserPassword);

            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail));
            message.From = new MailAddress(fromEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            client.Send(message);
        }
    }
}