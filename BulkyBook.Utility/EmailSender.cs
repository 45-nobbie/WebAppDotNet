using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using SparkPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }

        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //var emailToSend = new MimeMessage();
            //emailToSend.From.Add(MailboxAddress.Parse("hello@dotnetmastery.com"));
            //emailToSend.To.Add(MailboxAddress.Parse(email));
            //emailToSend.Subject = subject;
            //emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = htmlMessage};

            ////send email
            //using (var emailClient = new SmtpClient())
            //{
            //    emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //    emailClient.Authenticate("dotnetmastery@gmail.com", "DotNet123$");
            //    emailClient.Send(emailToSend);
            //    emailClient.Disconnect(true);
            //}

            //return Task.CompletedTask;

            
            var transmission = new Transmission();
            //var from = new EmailAddress("23.ramraja@gmail.com", "Bulky Book");
            transmission.Content.From.Email = "23.ramraja@gmail.com";
            /*var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject,"", htmlMessage);
            return client.SendEmailAsync(msg);*/
            transmission.Content.Subject = subject;
            //transmission.Content.Text = email;
            transmission.Content.Html = htmlMessage;

            var recipient = new Recipient
            {
                Address = new Address { Email = email }
            };
            transmission.Recipients.Add(recipient);
			var client = new Client(SendGridSecret);
            return client.Transmissions.Send(transmission);
		}
    }
}
