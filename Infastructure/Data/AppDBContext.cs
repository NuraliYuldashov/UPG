﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infastructure.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }

    public DbSet<Housing> Housings { get; set; }
    public DbSet<Keyboard> Keyboards { get; set; }
    public DbSet<Laptop> Laptops { get; set; }
    public DbSet<Mice> Mices { get; set; }
    public DbSet<Domain.Entities.Monitor> Monitors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}