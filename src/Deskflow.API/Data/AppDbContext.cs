using Deskflow.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Deskflow.API.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(category =>
            {
                category.ToTable("Categories");

                category.HasKey(e => e.Id);
                category.Property(e => e.Id)
                        .IsRequired();

                category.Property(e => e.Name)
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");
            });
        }
    }
}