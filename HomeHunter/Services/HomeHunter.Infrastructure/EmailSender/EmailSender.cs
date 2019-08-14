using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace HomeHunter.Infrastructure.EmailSender
{
    public class EmailSender : IApplicationEmailSender
    {
        private const string SenderEmail = "no-reply@homehunter.bg";
        private const string NameOfTheSender = "HomeHunter";

        private const string ContactFormEmailDestination = "writetorado@abv.bg";
        private const string ContactOfficialName = "Radoslav Vassilev";

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
                From = new EmailAddress(SenderEmail, NameOfTheSender),
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

        public Task SendContactFormEmailAsync(string email, string subject, string message)
        {
            return ContactFormEmailExecute(SendGridKey, subject, message, email);
        }

        private Task ContactFormEmailExecute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(email),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
            };
            msg.AddTo(new EmailAddress(ContactFormEmailDestination, ContactOfficialName));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
