using Microsoft.EntityFrameworkCore;
using GlamourZone.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;

namespace GlamourZone.Data
{
    public class SalonDbContext : DbContext
    {
        public SalonDbContext(DbContextOptions<SalonDbContext> options) : base(options) { }

        public DbSet<CategoryViewModel> Categories { get; set; }
        public DbSet<ServiceViewModel> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceViewModel>()
                .HasOne(s => s.Categories)          // Singular navigation property (Category)
                .WithMany(c => c.Services)        // Navigation property in CategoryViewModel
                .HasForeignKey(s => s.CategoryId) // Foreign Key in ServiceViewModel
                .OnDelete(DeleteBehavior.Cascade); // Optional, adjust based on your requirements
        }

    }
}
