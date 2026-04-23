using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookstoreApplication.Utils
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var editor1 = new ApplicationUser
            {
                UserName = "john",
                Email = "john.doe@example.com",
                Name = "John",
                Surname = "Doe",
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(editor1.UserName) == null)
            {
                await userManager.CreateAsync(editor1, "John123!");
                await userManager.AddToRoleAsync(editor1, "Editor");
            }

            var editor2 = new ApplicationUser
            {
                UserName = "jane",
                Email = "jane.doe@example.com",
                Name = "Jane",
                Surname = "Doe",
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(editor2.UserName) == null)
            {
                await userManager.CreateAsync(editor2, "Jane123!");
                await userManager.AddToRoleAsync(editor2, "Editor");
            }

            var editor3 = new ApplicationUser
            {
                UserName = "nick",
                Email = "nick.smith@example.com",
                Name = "Nick",
                Surname = "Smith",
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(editor3.UserName) == null)
            {
                await userManager.CreateAsync(editor3, "Nick123!");
                await userManager.AddToRoleAsync(editor3, "Editor");
            }
        }
    }
}
