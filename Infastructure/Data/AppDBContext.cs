﻿using Domain.Entities;
using DTOS.IdentitiesDTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Infastructure.Data;

public class AppDBContext : IdentityDbContext<ApplicationUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
    {
        //Database.EnsureCreated();
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Accessories> Accessories { get; set; }

    public DbSet<Armchairs> Armchairs { get; set; }
    public DbSet<Cooler> Coolers { get; set; }
    public DbSet<Drives> Drives { get; set; }
    public DbSet<GamingBuilds> GamingBuilds { get; set; }
    public DbSet<Headphones> Headphones { get; set; }

    public DbSet<Housing> Housings { get; set; }
    public DbSet<Keyboard> Keyboards { get; set; }
    public DbSet<Laptop> Laptops { get; set; }
    public DbSet<Mice> Mices { get; set; }
    public DbSet<Domain.Entities.Monitor> Monitors { get; set; }
    public DbSet<MousePads> Mouse_Pads { get; set; }
    public DbSet<RAM> RAMs { get; set; }
    public DbSet<TablesForGamers> Tables_For_Gamers { get; set; }
    public DbSet<PowerSupplies> Power_Supplies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(connectionString,
    //        options => options.MigrationsAssembly("Infastructure"));
    //}

}