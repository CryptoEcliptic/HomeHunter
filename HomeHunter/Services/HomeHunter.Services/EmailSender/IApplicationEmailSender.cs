using System.Threading.Tasks;

namespace HomeHunter.Services.EmailSender
{
    public interface IApplicationEmailSender
    {
        Task SendContactFormEmailAsync(string email, string subject, string message);

        Task SendEmailAsync(string email, string subject, string message);
    }
}
