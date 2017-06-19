using LeaderBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LeaderBoard.Models
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
                ))
            {
                if (!context.Record.Any())
                {
                    context.Record.AddRange(
                        new Record
                        {
                            Number = 2017200001,
                            Name = "张三",
                            Score = 88.8M,
                            Sex = "男"
                        },
                        new Record
                        {
                            Number = 2017200002,
                            Name = "李四",
                            Score = 89.2M,
                            Sex = "女"
                        }
                        );
                    context.SaveChanges();
                    //return;
                }
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] Roles = new string[] { "Administrator", "User" };
                for (int i = 0; i < Roles.Length; i++)
                {
                    if (!await roleManager.RoleExistsAsync(Roles[i]))
                    {
                        await roleManager.CreateAsync(new IdentityRole(Roles[i]));
                    }
                }
            }
        }
    }
}
