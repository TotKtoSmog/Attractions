using Attractions.Models.dboModels;
using Microsoft.EntityFrameworkCore;

namespace Attractions.Dbcontext
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options)
        : base(options)
        {
        }
        dbContext()
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(e => e.Id)
                .HasColumnName("id");
                entity.Property(e => e.FirstName)
                .HasColumnName("firstname")
                .HasMaxLength(30);
                entity.Property(e => e.LastName)
                .HasColumnName("lastname")
                .HasMaxLength(30);
                entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(30);
                entity.Property(e => e.Password)
                .HasColumnName("passwordhash")
                .HasMaxLength(120);
                entity.Property(e => e.Age)
                .HasColumnName("age");
            });
        }
    }
}

