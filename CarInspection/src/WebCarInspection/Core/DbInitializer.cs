using System;
using System.Threading.Tasks;
using DataAccessLayer.DTO.DB;
using Microsoft.AspNetCore.Identity;

namespace WebCarInspection.Core
{
    public static class DbInitializer
    {
        public static Task InitializeAsync(UserManager<UserDto> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            if (roleManager is null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            return InitializeInternal(userManager, roleManager);
        }

        private static async Task InitializeInternal(UserManager<UserDto> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(RoleNames.Administrator) is null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleNames.Administrator));
            }

            if (await roleManager.FindByNameAsync(RoleNames.User) is null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleNames.User));
            }

            await InitialProfils(userManager);
        }

        private static async Task InitialProfils(UserManager<UserDto> userManager)
        {
            string userName = "Solinx";
            string password = "_1User2";

            if (await userManager.FindByNameAsync(userName) is null)
            {
                var user = new UserDto
                {
                    FirstName = "Oleg",
                    Surname = "Fedosov",
                    Patronic = "Vasilyevich",
                    UserName = userName,
                };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleNames.Administrator);
                }
            }

            userName = "MainUser";
            password = "7_Event_14";

            if (await userManager.FindByNameAsync(userName) is null)
            {
                var user = new UserDto
                {
                    FirstName = "Dima",
                    Surname = "Gobalov",
                    Patronic = "Grabovich",
                    UserName = userName,
                };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleNames.User);
                }
            }
        }
    }
}
