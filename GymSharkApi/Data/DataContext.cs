using GymSharkApi.Entities;
using GymSharkAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> Orders { get; set; }
        public DbSet<Opinion> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductOrder>()
                .HasKey(k => new { k.SourceUsertId, k.OrderedProductId });

            builder.Entity<ProductOrder>()
                .HasOne(s => s.SourceUser)
                .WithMany(o => o.OrderedProducts)
                .HasForeignKey(s => s.SourceUsertId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductOrder>()
                .HasOne(s => s.OrderedProduct)
                .WithMany(o => o.OrderedByUsers)
                .HasForeignKey(s => s.OrderedProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Opinion>()
                .HasOne(p => p.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Opinion>()
                .HasOne(s => s.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
