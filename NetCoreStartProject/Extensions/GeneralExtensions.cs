using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;
using Slugify;

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


        public static async Task<string> GenrateEmailConfirmationUrlAsync(this UserManager<User> userManager , User User , IUrlHelper Url , string RequestSchema)
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

        public static async Task<string> GenrateForgetPasswardUrlAsync(this UserManager<User> userManager, User User, IUrlHelper Url, string RequestSchema)
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

        public static string GenrateSlug(this string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }

            // Creating a configuration object
            var config = new SlugHelperConfiguration();

            // Replace spaces with a dash
            config.StringReplacements.Add(" ", "-");

            // We want a lowercase Slug
            config.ForceLowerCase = true;

            // Will collapse multiple seqential dashes down to a single one
            config.CollapseDashes = true;

            // Will trim leading and trailing whitespace
            config.TrimWhitespace = true;

            // Colapse consecutive whitespace chars into one
            config.CollapseWhiteSpace = true;

            // Remove everything that's not a letter, number, hyphen, dot, or underscore
            config.DeniedCharactersRegex = @"[^a-zA-Z0-9\-\._]";

            SlugHelper helper = new SlugHelper(config);

            return helper.GenerateSlug(title); // "ola-ke-ase"
            
        }
    }
}