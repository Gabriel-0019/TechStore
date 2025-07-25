using Entities.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TechStoreDBContext : DbContext
    {
        public TechStoreDBContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechStoreDBContext>();
            optionsBuilder.UseNpgsql(Util.ConnectionString);
        }

        public TechStoreDBContext(DbContextOptions<TechStoreDBContext> options) : base(options)
        {
        }

        public DbSet<PassResetToken> PassResetTokens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Util.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                      .HasColumnName("Email")
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.Property(e => e.Password)
                      .HasColumnName("Password")
                      .IsRequired()
                      .HasMaxLength(65);
            });

            modelBuilder.Entity<PassResetToken>(entity =>
            {
                entity.ToTable("PassResetTokens");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Token)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("timestamp")
                      .IsRequired();

                modelBuilder.Entity<PassResetToken>()
                    .Property(p => p.UserId)
                    .HasColumnName("UserID");

            });
        }
    }
}
