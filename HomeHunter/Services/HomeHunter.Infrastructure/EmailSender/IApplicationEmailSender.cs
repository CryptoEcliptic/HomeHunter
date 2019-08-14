using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunter.Infrastructure.EmailSender
{
    public interface IApplicationEmailSender : IEmailSender
    {
        Task SendContactFormEmailAsync(string email, string subject, string message);
    }
}
