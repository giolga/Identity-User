using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;

namespace IdentityApp.Data
{
    public class SeedData
    {
        private static async Task<string> EnsureUser( //this method creates new acc
            IServiceProvider serviceProvider,
            string initPw, string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, initPw);
            }

            if (user is null)
            {
                throw new Exception("User did not get created. Password policy problem?");
            }

            return user.Id;
        }

        public static async Task<IdentityResult> EnsureRole(
                IServiceProvider serviceProvider,
                string uid,
                string role
            )
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            IdentityResult ir;

            if (await roleManager.RoleExistsAsync(role) == false)
            {
                ir = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user is null)
            {
                throw new Exception("User not existing!");
            }

            ir = await userManager.AddToRoleAsync(user, role);

            return ir;
        }

    }
}
