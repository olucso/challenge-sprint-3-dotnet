using Microsoft.EntityFrameworkCore;
using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<ClientEntity> Client { get; set; }
        public DbSet<MotorcycleEntity> Motorcycle { get; set; }
        public DbSet<RentalEntity> Rental { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentalEntity>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalEntity>()
                .HasOne(r => r.Motorcycle)
                .WithMany(m => m.Rentals)
                .HasForeignKey(r => r.MotorcycleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MotorcycleEntity>()
                .Property(m => m.DailyValue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MotorcycleEntity>()
                .Property(m => m.Availability)
                .HasConversion<int>();

            modelBuilder.Entity<RentalEntity>()
                .Property(r => r.TotalValue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<RentalEntity>()
                .Property(r => r.Completed)
                .HasConversion<int>();
        }
    }
}
