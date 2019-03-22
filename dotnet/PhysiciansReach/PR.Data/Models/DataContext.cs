using Microsoft.EntityFrameworkCore;

namespace PR.Data.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<UserAccount> UserAccount { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Physician> Physician { get; set; }

        public DbSet<Agent> Agent { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserAccountBuilder(modelBuilder);
            AdminBuilder(modelBuilder);
            PhysicianBuilder(modelBuilder);
            AgentBuilder(modelBuilder);
        }

        protected void UserAccountBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount", "dbo");

                entity.HasKey(e => e.UserAccountId).ForSqlServerIsClustered(false);

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(200);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            });
        }

        protected void AdminBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin", "dbo");

                entity.HasKey(e => e.AdminId).ForSqlServerIsClustered(false);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.UserAccount)
                     .WithOne(p => p.Admin)
                     .HasForeignKey<Admin>(b => b.UserAccountId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Admin_UserAccount");

            });
        }

        protected void PhysicianBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Physician>(entity =>
            {
                entity.ToTable("Physician", "dbo");

                entity.HasKey(e => e.PhysicianId).ForSqlServerIsClustered(false);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(10);

                entity.Property(e => e.ContactFirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.ContactLastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            });
        }

        protected void AgentBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agent", "dbo");

                entity.HasKey(e => e.AgentId).ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.VendorId);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.UserAccount)
                     .WithOne(p => p.Agent)
                     .HasForeignKey<Agent>(b => b.UserAccountId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Agent_UserAccount");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agent_Vendor");
            });
        }

        protected void VendorBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor", "dbo");

                entity.HasKey(e => e.VendorId).ForSqlServerIsClustered(false);

                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.DoingBusinessAs).IsRequired().HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(10);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            });
        }
    }
}
