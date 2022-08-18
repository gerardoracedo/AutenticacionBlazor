using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Servicios
{
    public interface ISendMail
    {
        Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody);
    }
}