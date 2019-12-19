using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreStartProject.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }


        public static async Task<string> GenrateEmailConfirmationUrlAsync(this UserManager<IdentityUser> userManager , IdentityUser User , IUrlHelper Url , string RequestSchema)
        {
            if (User == null || string.IsNullOrEmpty(User.Id))
            {
                return string.Empty;
            }
            var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(User);
            string url = Url.Action("ConfirmEmail", "Identity",
                                    new { userId = User.Id, token = emailConfirmationToken }, RequestSchema);
            return url;
        }

        public static async Task<string> GenrateForgetPasswardUrlAsync(this UserManager<IdentityUser> userManager, IdentityUser User, IUrlHelper Url, string RequestSchema)
        {
            if (User == null || string.IsNullOrEmpty(User.Id))
            {
                return string.Empty;
            }
            var emailConfirmationToken = await userManager.GeneratePasswordResetTokenAsync(User);
            string url = Url.Action("ConfirmEmail", "Identity",
                                    new { userId = User.Id, token = emailConfirmationToken }, RequestSchema);
            return url;
        }
    }
}