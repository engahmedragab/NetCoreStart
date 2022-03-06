using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Installers
{
    public class RoleInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //initializing custom roles 
            //var RoleManager = (RoleManager<Role>)services.GetService<RoleManager<Role>>();
            //var UserManager = services.GetService<UserManager<User>>();
            //string[] roleNames = { "Admin", "Store-Manager", "Member" };
            //IdentityResult roleResult;

            //foreach (var roleName in roleNames)
            //{
            //    var roleExist = await RoleManager.RoleExistsAsync(roleName);
            //    // ensure that the role does not exist
            //    if (!roleExist)
            //    {
            //        //create the roles and seed them to the database: 
            //        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            //    }
            //}

            //// find the user with the admin email 
            //var _user = await UserManager.FindByEmailAsync("admin@email.com");

            //// check if the user exists
            //if (_user == null)
            //{
            //    //Here you could create the super admin who will maintain the web app
            //    var poweruser = new ApplicationUser
            //    {
            //        UserName = "Admin",
            //        Email = "admin@email.com",
            //    };
            //    string adminPassword = "p@$$w0rd";

            //    var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
            //    if (createPowerUser.Succeeded)
            //    {
            //        //here we tie the new user to the role
            //        await UserManager.AddToRoleAsync(poweruser, "Admin");

            //    }
            //}
        }

    }
}
