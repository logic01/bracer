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

        public DbSet<Address> Address { get; set; }

        public DbSet<Log> Log { get; set; }

        public DbSet<Patient> Patient { get; set; }

        public DbSet<IntakeForm> IntakeForm { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Answer> Answer { get; set; }

        public DbSet<Document> Document { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserAccountBuilder(modelBuilder);
            AdminBuilder(modelBuilder);
            PhysicianBuilder(modelBuilder);
            AgentBuilder(modelBuilder);
            PatientBuilder(modelBuilder);
            AddressBuilder(modelBuilder);
            LogBuilder(modelBuilder);
            IntakeFormBuilder(modelBuilder);
            QuestionBuilder(modelBuilder);
            AnswerBuilder(modelBuilder);
            DocumentBuilder(modelBuilder);
        }

        protected void LogBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log", "dbo");

                entity.Property(e => e.Severity).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.HasKey(e => e.LogId).ForSqlServerIsClustered(false);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.StackTrace).IsRequired();

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");
            });
        }

        protected void UserAccountBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount", "dbo");

                entity.Property(e => e.Type).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.HasKey(e => e.UserAccountId).ForSqlServerIsClustered(false);

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(200);

                entity.Property(e => e.EmailAddress).IsRequired().HasMaxLength(100).HasDefaultValue("test@test.com");

                entity.Property(e => e.Active).IsRequired().HasDefaultValue(true);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasIndex(e => e.UserName).IsUnique();
            });
        }

        protected void AdminBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin", "dbo");

                entity.HasKey(e => e.UserAccountId).ForSqlServerIsClustered(false);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.UserAccount)
                     .WithOne(p => p.Admin)
                     .HasForeignKey<Admin>(b => b.UserAccountId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Admin_UserAccount");

            });
        }

        protected void AddressBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "dbo");

                entity.HasKey(e => e.AddressId).ForSqlServerIsClustered(false);

                entity.Property(e => e.AddressLineOne).HasMaxLength(100);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(100);

                entity.Property(e => e.City).IsRequired().HasMaxLength(100);

                entity.Property(e => e.State).IsRequired().HasMaxLength(100);

                entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

            });
        }

        protected void PhysicianBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Physician>(entity =>
            {
                entity.ToTable("Physician", "dbo");

                entity.HasKey(e => e.UserAccountId).ForSqlServerIsClustered(false);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(10);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.UserAccount)
                     .WithOne(p => p.Physician)
                     .HasForeignKey<Physician>(b => b.UserAccountId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Physician_UserAccount");

                entity.HasOne(d => d.Address)
                     .WithOne(p => p.Physician)
                     .HasForeignKey<Physician>(b => b.AddressId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Physician_Address");
            });
        }

        protected void AgentBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agent", "dbo");

                entity.HasKey(e => e.UserAccountId).ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.VendorId);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

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

                entity.Property(e => e.ContactFirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.ContactLastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");
            });
        }

        protected void PatientBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient", "dbo");

                entity.HasKey(e => e.PatientId).ForSqlServerIsClustered(false);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(10);

                entity.Property(e => e.DateOfBirth).IsRequired().HasColumnType("datetime2");

                entity.Property(e => e.Language).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.BestTimeToCallBack).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.Therapy).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.Insurance).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.Pharmacy).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.Sex).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.CallBackImmediately).IsRequired().HasDefaultValue(true);

                entity.Property(e => e.IsDme).IsRequired().HasDefaultValue(false);

                entity.Property(e => e.BestTimeToCallBack).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.Medications).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Notes).IsRequired().HasMaxLength(100);

                entity.Property(e => e.OtherProducts).IsRequired().HasMaxLength(100);

                entity.Property(e => e.PhysiciansName).IsRequired().HasMaxLength(10);

                entity.Property(e => e.PhysiciansPhoneNumber).IsRequired().HasMaxLength(10);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Agent)
                     .WithMany(p => p.Patients)
                     .HasForeignKey(b => b.AgentId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Patient_Agent");

                entity.HasOne(d => d.Address)
                     .WithOne(p => p.Patient)
                     .HasForeignKey<Patient>(b => b.AddressId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Patient_Address");

                entity.HasOne(d => d.PhysiciansAddress)
                     .WithOne(p => p.PatientsPhysician)
                     .HasForeignKey<Patient>(b => b.PhysiciansAddressId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Patient_Physicians_Address");
            });
        }

        protected void IntakeFormBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntakeForm>(entity =>
            {
                entity.ToTable("IntakeForm", "dbo");

                entity.Property(e => e.IntakeFormType).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.HasKey(e => e.IntakeFormId).ForSqlServerIsClustered(false);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Patient)
                     .WithMany(p => p.IntakeForms)
                     .HasForeignKey(b => b.PatientId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Patient_IntakeForms");
            });
        }

        protected void QuestionBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question", "dbo");

                entity.HasKey(e => e.QuestionId).ForSqlServerIsClustered(false);

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IntakeForm)
                     .WithMany(p => p.Questions)
                     .HasForeignKey(b => b.IntakeFormId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_IntakeForm_Questions");
            });
        }

        protected void AnswerBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer", "dbo");

                entity.HasKey(e => e.AnswerId).ForSqlServerIsClustered(false);

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Question)
                     .WithMany(p => p.Answers)
                     .HasForeignKey(b => b.QuestionId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Questions_Answers");
            });
        }

        protected void DocumentBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document", "dbo");

                entity.Property(e => e.Type).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.Status).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.HasKey(e => e.DocumentId).ForSqlServerIsClustered(false);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IntakeForm)
                     .WithMany(p => p.Documents)
                     .HasForeignKey(b => b.IntakeFormId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_IntakeForm_Documents");

                entity.HasOne(d => d.Physician)
                     .WithMany(p => p.Documents)
                     .HasForeignKey(b => b.PhysicianId)
                     .IsRequired(false)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Physician_Documents");
            });
        }
    }
}
