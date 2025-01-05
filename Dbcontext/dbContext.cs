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
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Sight> Sight { get; set; }
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
                .HasColumnName("password")
                .HasMaxLength(120);
                entity.Property(e => e.Age)
                .HasColumnName("age");
                entity.Property(e => e.UserType)
                .HasColumnName("usertype");
            });
            modelBuilder.Entity<City>(entity => 
            {
                entity.ToTable("city");
                entity.Property(e => e.Id)
                .HasColumnName("id");
                entity.Property(e => e.CityName)
                .HasColumnName("cityname");
            });
            modelBuilder.Entity<Sight>(entity =>
            {
                entity.ToTable("sight");
                entity.Property(e => e.Id)
                .HasColumnName("id");
                entity.Property(e => e.SightName)
                .HasColumnName("sightname");
                entity.Property(e => e.AvgBall)
                .HasColumnName("avgball");
                entity.Property(e => e.Id_City)
                .HasColumnName("id_city");
            });
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");
                entity.Property(e => e.Id)
                .HasColumnName("id");
                entity.Property(e => e.NameSender)
                .HasColumnName("namesender");
                entity.Property(e => e.FeedBackText)
                .HasColumnName("feedbacktext");
                entity.Property(e => e.Ball)
                .HasColumnName("ball");
                entity.Property(e => e.fb_datatime)
                .HasColumnName("feedback_datatime");
                entity.Property(e => e.IsAccepted)
                .HasColumnName("accepted");
                entity.Property(e => e.Id_Sight)
                .HasColumnName("id_sight");
                entity.Property(e => e.Id_User)
                .HasColumnName("id_users");
            });
        }
    }
}

