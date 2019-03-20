using Microsoft.EntityFrameworkCore;

namespace PR.Data.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "dbo");

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(200);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(10);

                entity.Property(e => e.ContactFirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.ContactLastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            });

        }
    }
}
