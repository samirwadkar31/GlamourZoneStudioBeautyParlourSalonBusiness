// Data/SalonDbContext.cs
using GlamourZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GlamourZone.Data
{
    public class SalonDbContext : DbContext
    {
        public SalonDbContext(DbContextOptions<SalonDbContext> options) : base(options) { }

        public DbSet<ServiceViewModel> Services { get; set; }
        public DbSet<CategoryViewModel> Categories { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentService> AppointmentServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppointmentService>()
                .HasKey(aps => new { aps.AppointmentId, aps.ServiceId });

            modelBuilder.Entity<AppointmentService>()
                .HasOne(aps => aps.Appointment)
                .WithMany(a => a.AppointmentServices)
                .HasForeignKey(aps => aps.AppointmentId);

            modelBuilder.Entity<AppointmentService>()
                .HasOne(aps => aps.Service)
                .WithMany(s => s.AppointmentServices)
                .HasForeignKey(aps => aps.ServiceId);

            modelBuilder.Entity<ServiceViewModel>()
                .HasOne(s => s.Categories)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}