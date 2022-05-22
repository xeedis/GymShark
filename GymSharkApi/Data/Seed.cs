using GymSharkApi.Entities;
using GymSharkAPI.Data;
using GymSharkAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymSharkApi.Data
{
    public class Seed
    {
        public static async Task SeedProduct(DataContext context)
        {
            if (await context.Products.AnyAsync()) return;

            var productsData = await System.IO.File.ReadAllTextAsync("./Data/ProductSeedData.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            foreach (var product in products)
            {
                product.ProductName = product.ProductName.ToLower();

                context.Products.Add(product);
            }
            await context.SaveChangesAsync();
        }
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var usersData = await System.IO.File.ReadAllTextAsync("./Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(usersData);
            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            await context.SaveChangesAsync();       
        }
    }
}
