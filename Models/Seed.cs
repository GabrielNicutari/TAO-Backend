using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAO_Backend.Models
{
    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser{ DisplayName = "Bob", UserName = "bob", Email = "bob@test.com"},
                    new AppUser{ DisplayName = "Tom", UserName = "tom", Email = "tom@test.com"},
                    new AppUser{ DisplayName = "Jane", UserName = "jane", Email = "jane@test.com"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$word1");
                    System.Diagnostics.Debug.WriteLine("Created");
                }
            }
        }
    }
}
