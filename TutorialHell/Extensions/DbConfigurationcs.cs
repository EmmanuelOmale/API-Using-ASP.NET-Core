using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TutorialHell.Data;

namespace WebContactAPI.Extensions
{
    public static class DbConfigurationcs
    {
        public static void IdentityRegistration(this IServiceCollection services, IConfiguration config)
        {
            // For Entity Framework
            services.AddDbContext<ContactsDbContext>(options => options.UseSqlServer(config.GetConnectionString("ContactsApiAConnectionString")));

            // For Identity
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
            }).

                  AddEntityFrameworkStores<ContactsDbContext>()
                 .AddRoles<IdentityRole>()
                 .AddRoleManager<RoleManager<IdentityRole>>()
                 .AddUserManager<UserManager<User>>()
                 .AddSignInManager<SignInManager<User>>()
                 .AddDefaultTokenProviders();



        }
    }
}

