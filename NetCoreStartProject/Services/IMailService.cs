using NetCoreStartProject.Contracts.V1.Requests;
using System.Threading.Tasks;

namespace NetCoreStartProject.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
