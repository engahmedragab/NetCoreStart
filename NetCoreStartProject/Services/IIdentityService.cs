using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreStartProject.Domain;
using NetCoreStartProject.Enums;

namespace NetCoreStartProject.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, IUrlHelper url = null, string reqestSchema = "");
        
        Task<AuthenticationResult> LoginAsync(string email, string password, IUrlHelper url = null, string reqestSchema = "");

        Task<AuthenticationResult> LoginWithExternalProvidersAsync(string accessToken, ExternalProvidersType externalProvidersType);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

        Task<AuthenticationResult> ConfirmEmailAsync(string userId, string confirmMailToken);

        Task<AuthenticationResult> IsEmailInUseAsync(string email);

        Task<AuthenticationResult> HasPasswordAsync(string userId);

        Task<AuthenticationResult> AddPasswordAsync(string userId, string password);

        Task<AuthenticationResult> ChangePasswordAsync(string userId , string password , string newPassword);

        Task<AuthenticationResult> ForgotPasswordAsync(string userEmail, IUrlHelper url = null, string reqestSchema = "");

        Task<AuthenticationResult> ResetPasswordAsync(string userEmail, string token , string password);
    }
}