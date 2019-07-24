﻿using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace HomeHunter.Infrastructure.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private const string SENDER_EMAIL = "no-reply@homehunter.bg";
        private const string NAME_OF_THE_SENDER = "HomeHunter";

        public EmailSender(IConfiguration Configuration)
        {
            this.SendGridKey = Configuration["SENDGRID_API_KEY"];
        }

        public string SendGridUser { get; set; }

        public string SendGridKey { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(this.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(SENDER_EMAIL, NAME_OF_THE_SENDER),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}