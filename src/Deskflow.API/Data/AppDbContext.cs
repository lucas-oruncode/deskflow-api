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
        public DbSet<Ticket> Tickets => Set<Ticket>();

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

                category.HasMany(c => c.Tickets)
                        .WithOne(t => t.Category)
                        .HasForeignKey(t => t.CategoryId);
            });

            modelBuilder.Entity<Ticket>(ticket =>
            {
                ticket.ToTable("Tickets");

                ticket.HasKey(e => e.Id);
                ticket.Property(e => e.Id)
                      .IsRequired();

                ticket.Property(e => e.Title)
                      .IsRequired()
                      .HasColumnType("nvarchar(50)");

                ticket.Property(e => e.Description)
                      .IsRequired()
                      .HasColumnType("nvarchar(200)");

                ticket.Property(e => e.Requester)
                      .IsRequired()
                      .HasColumnType("nvarchar(50)");

                ticket.Property(e => e.Priority)
                      .IsRequired();

                ticket.Property(e => e.Status)
                      .IsRequired();
                
                ticket.Property(t => t.Solution)
                      .HasColumnType("nvarchar(500)");

                ticket.Property(e => e.CreatedAt)
                      .IsRequired();

                ticket.Property(e => e.ClosedAt);
            });
        }
    }
}