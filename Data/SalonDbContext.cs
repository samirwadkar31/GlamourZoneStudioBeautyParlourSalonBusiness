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


    }
}
