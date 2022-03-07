using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using Todo.Web.Utilities;

namespace Todo.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = SmtpSettings.Gmail_Host;
            string fromPassword = SmtpSettings.Gmail_Password;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(SmtpSettings.User);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></email>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);

            //string body = "<html><body> " + htmlMessage + " </body></email>";
            //var message = new MailMessage(SmtpSettings.User, email, subject, body);
            //using (var emailClient = new SmtpClient(SmtpSettings.Host, SmtpSettings.Port))
            //{
            //    emailClient.Credentials = new NetworkCredential(SmtpSettings.User,
            //                                                    SmtpSettings.Password);
            //    await emailClient.SendMailAsync(message);
            //}
        }
    }
}
