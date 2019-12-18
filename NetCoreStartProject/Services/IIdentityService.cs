using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, IUrlHelper url = null, string reqestSchema = "");
        
        Task<AuthenticationResult> LoginAsync(string email, string password, IUrlHelper url = null, string reqestSchema = "");
        
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

        Task<AuthenticationResult> ConfirmEmailAsync(string userId, string confirmMailToken);
        
    }
}