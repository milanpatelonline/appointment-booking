using AppointmentBooking.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace AppointmentBooking.API.Helpers
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Applicant", "Staff", "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var email = "admin@site.com";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = "Admin",
                    LastName = "User"
                };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
