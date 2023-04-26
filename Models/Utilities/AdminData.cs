using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Models.Utilities
{
    public class AdminData
    {
        public static async Task Seeder(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Regular" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));

                    }
                }

                using (var UserN = applicationBuilder.ApplicationServices.CreateAsyncScope())

                {
                    var userManager = UserN.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var userName = "Emma";
                    var Password = "Emma123@$";
                    if (await userManager.FindByNameAsync(userName) == null)
                    {
                        var user = new User();
                        user.UserName = userName;
                        user.Password = Password;

                        await userManager.CreateAsync(user, Password);
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }
    }
}
