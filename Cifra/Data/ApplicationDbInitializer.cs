using Cifra.Data.Static;
using Cifra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using System.Security.Policy;
using System;

namespace Cifra.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                dbContext.Database.EnsureCreated();

                // Platforms
                if (!dbContext.Views.Any())
                {
                    dbContext.Views.AddRange(new List<View>()
                    {
                        new View()
                        {
                            Name = "Инфрокрасный",
                            Description = "Что-то позволяет, а что секрет"
                        },
                        new View()
                        {
                            Name = "Красный",
                            Description = "Красивый"
                        }
                    });
                    dbContext.SaveChanges();
                }
                // Publishers
                if (!dbContext.Countries.Any())
                {
                    dbContext.Countries.AddRange(new List<Country>()
                    {
                        new Country()
                        {
                            Logo = "https://flagof.ru/wp-content/uploads/2018/10/%D0%9A%D0%9D%D0%A0_1.jpg",
                            Name = "Китай"
                        }
                    });
                    dbContext.SaveChanges();
                }
                // Developers
                if (!dbContext.Manufacturers.Any())
                {
                    dbContext.Manufacturers.AddRange(new List<Manufacturer>
                    {
                        new Manufacturer()
                        {
                            ProfilePictureURL = "https://assets.turbologo.ru/blog/ru/2021/06/03082655/logo-apple-1998.png",
                            FullName = "Apple"
                        }
                    });
                    dbContext.SaveChanges();
                }
                // Games
                if (!dbContext.Products.Any())
                {
                    dbContext.Products.AddRange(new List<Product>
                    {
                        new Product()
                        {
                            ImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSeDIHPX81mIx5R9SSmsjJZqg8deFUPInSjig&usqp=CAU",
                            Name = "Чехол",
                            Description = "Чехол лучший",
                            Price = 780,
                            ReleaseDate = new DateTime(2022,10,27),
                            Category = Enums.ProductCategory.Phone_Case,
                            ViewId = 1,
                            CountryId = 1
                        }
                    });
                    dbContext.SaveChanges();
                }
                // Developers & Games
                if (!dbContext.Manufacturers_Products.Any())
                {
                    dbContext.Manufacturers_Products.AddRange(new List<Manufacturer_Product>
                    {
                        new Manufacturer_Product()
                        {
                            ManufacturerId = 1,
                            ProductId = 1
                        }
                    });
                    dbContext.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                // Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Admin user
                const string adminEmail = "admin@egames.com";
                const string adminPassword = "Admin@123$";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin",
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true,
                    };
                    // User, Password
                    var result = await userManager.CreateAsync(newAdminUser, adminPassword);

                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                // Normal user
                const string userEmail = "user@egames.com";
                const string userPassword = "User@123$";

                var user = await userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        FullName = "App User",
                        UserName = "app-user",
                        Email = userEmail,
                        EmailConfirmed = true,
                    };
                    // User, Password
                    var result = await userManager.CreateAsync(newUser, userPassword);

                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}
