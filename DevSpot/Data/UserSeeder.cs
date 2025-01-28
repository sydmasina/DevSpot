using Microsoft.AspNetCore.Identity;
using DevSpot.Constants;

namespace DevSpot.Data
{
    public class UserSeeder
    {
        public static async Task SeedUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateUserWithRole(userManager, "admin@dev.test", "Admin123!", Roles.Admin);
            await CreateUserWithRole(userManager, "jobseeker@dev.test", "Jobseeker123!", Roles.JobSeeker);
            await CreateUserWithRole(userManager, "employer@dev.test", "Employer123!", Roles.Employer);
        }

        private static async Task CreateUserWithRole(
            UserManager<IdentityUser> userManager,
            string email,
            string password,
            string role
            )
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Error: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
