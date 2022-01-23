﻿using BlazorStack.Shared;
using BlazorStack.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorStack.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
