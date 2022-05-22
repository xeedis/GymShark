﻿using GymSharkApi.Entities;
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
    }
}
