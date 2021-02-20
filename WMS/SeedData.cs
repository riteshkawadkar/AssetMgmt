﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<Employee> userManager)
        {
            var users = userManager.GetUsersInRoleAsync("Employee").Result;

            if (userManager.FindByNameAsync("superadmin@localhost.com").Result == null)
            {
                var user = new Employee
                {
                    UserName = "superadmin@localhost.com",
                    Email = "superadmin@localhost.com",
                    Firstname = "OrigamiITLab",
                    Lastname = "Vendor",
                    DateOfBirth = DateTime.Now,
                    DateJoined = DateTime.Now
                    
                };
                var result = userManager.CreateAsync(user, "P@ssword1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
            }
            if (userManager.FindByNameAsync("admin@localhost.com").Result == null)
            {
                var user = new Employee
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com",
                    Firstname = "Company",
                    Lastname = "Admin",
                    DateOfBirth = DateTime.Now,
                    DateJoined = DateTime.Now
                };
                var result = userManager.CreateAsync(user, "P@ssword1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "SuperAdmin"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
