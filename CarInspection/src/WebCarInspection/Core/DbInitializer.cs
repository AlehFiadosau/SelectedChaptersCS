using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Core
{
    public static class DbInitializer
    {
        public static Task InitializeAsync(UserManager<UserViewModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            return InitializeInternal(userManager, roleManager);
        }

        private static async Task InitializeInternal(UserManager<UserViewModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            await InitialProfils(userManager);
        }

        private static async Task InitialProfils(UserManager<UserViewModel> userManager)
        {
            string userName = "Solinx";
            string password = "_1User2";

            if (await userManager.FindByNameAsync(userName) == null)
            {
                var user = new UserViewModel
                {
                    FirstName = "Oleg",
                    Surname = "Fedosov",
                    Patronic = "Vasilyevich",
                    UserName = userName,
                };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

            userName = "MainUser";
            password = "7_Event_14";

            if (await userManager.FindByNameAsync(userName) == null)
            {
                var user = new UserViewModel
                {
                    FirstName = "Dima",
                    Surname = "Gobalov",
                    Patronic = "Grabovich",
                    UserName = userName,
                };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
