using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace TAO_Backend.Models
{
    public partial class DBContext : DbContext
    {
        private readonly IConfiguration _config;
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DailyReading> DailyReadings { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(_config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyReading>(entity =>
            {
                entity.HasOne(d => d.HouseReading)
                    .WithMany(p => p.DailyReadings)
                    .HasForeignKey(d => d.HouseReadingId)
                    .HasConstraintName("house_reading_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.House)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.HouseId)
                    .HasConstraintName("house_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
