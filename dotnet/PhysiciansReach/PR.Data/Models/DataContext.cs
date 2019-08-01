using Microsoft.EntityFrameworkCore;
using PR.Constants.Enums;

namespace PR.Data.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        private readonly string connectionString;
        /// <summary>
        /// Added this constructor to allow for integration tests
        /// </summary>
        /// <param name="connectionString"></param>
        public DataContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

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

        public DbSet<Signature> Signature { get; set; }

        public DbSet<ICD10Code> ICD10Code { get; set; }

        public DbSet<PrivateInsurance> PrivateInsurance { get; set; }

        public DbSet<Medicare> Medicare { get; set; }

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
            SignatureBuilder(modelBuilder);
            PrivateInsuranceBuilder(modelBuilder);
            MedicareBuilder(modelBuilder);
            ICD10CodeBuilder(modelBuilder);
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

                // maybe this should be required?
                entity.Property(e => e.DEA).HasMaxLength(100);

                // maybe this should be required?
                entity.Property(e => e.NPI).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(10);

                entity.Property(e => e.FaxNumber).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.UserAccount)
                     .WithOne(p => p.Physician)
                     .HasForeignKey<Physician>(b => b.UserAccountId)
                     .HasConstraintName("FK_Physician_UserAccount");

                entity.HasOne(d => d.Address)
                     .WithOne(p => p.Physician)
                     .HasForeignKey<Physician>(b => b.AddressId)
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
                     .HasConstraintName("FK_Agent_UserAccount");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.VendorId)
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

                entity.Property(e => e.Active).IsRequired().HasDefaultValue(true);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");
            });
        }

        protected void MedicareBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicare>(entity =>
            {
                entity.ToTable("Medicare", "dbo");

                entity.HasKey(e => e.MedicareId).ForSqlServerIsClustered(false);
                entity.Property(e => e.MemberId).HasMaxLength(100);
                entity.Property(e => e.PatientGroup).HasMaxLength(100);
                entity.Property(e => e.Pcn).HasMaxLength(100);
                entity.Property(e => e.SubscriberNumber).HasMaxLength(100);
                entity.Property(e => e.SecondaryCarrier).HasMaxLength(100);
                entity.Property(e => e.SecondarySubscriberNumber).HasMaxLength(100);

            });
        }

        protected void PrivateInsuranceBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrivateInsurance>(entity =>
            {
                entity.ToTable("PrivateInsurance", "dbo");

                entity.HasKey(e => e.PrivateInsuranceId).ForSqlServerIsClustered(false);
                entity.Property(e => e.Insurance).HasMaxLength(100);
                entity.Property(e => e.InsuranceId).HasMaxLength(100);
                entity.Property(e => e.Group).HasMaxLength(100);
                entity.Property(e => e.PCN).HasMaxLength(100);
                entity.Property(e => e.Bin).HasMaxLength(100);
                entity.Property(e => e.Street).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(30);
                entity.Property(e => e.State).HasMaxLength(2);
                entity.Property(e => e.Zip).HasMaxLength(10);
                entity.Property(e => e.Phone).HasMaxLength(10);

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

                entity.Property(e => e.Sex).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.IsDme).IsRequired().HasDefaultValue(false);

                entity.Property(e => e.Waist).HasMaxLength(3);
                entity.Property(e => e.Height).HasMaxLength(6);
                entity.Property(e => e.ShoeSize).HasMaxLength(4);
                entity.Property(e => e.Allergies).HasMaxLength(500);
                entity.Property(e => e.Weight).HasMaxLength(3);

                entity.Property(e => e.Medications).HasMaxLength(100);

                entity.Property(e => e.Notes).HasMaxLength(100);

                entity.Property(e => e.OtherProducts).HasMaxLength(100);

                entity.Property(e => e.PhysiciansName).HasMaxLength(100);

                entity.Property(e => e.PhysiciansPhoneNumber).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Agent)
                     .WithMany(p => p.Patients)
                     .HasForeignKey(b => b.AgentId)
                       .HasConstraintName("FK_Patient_Agent");

                entity.HasOne(d => d.Address)
                     .WithOne(p => p.Patient)
                     .HasForeignKey<Patient>(b => b.AddressId)
                     .HasConstraintName("FK_Patient_Address");

                entity.HasOne(d => d.PhysiciansAddress)
                     .WithOne(p => p.PatientsPhysician)
                     .HasForeignKey<Patient>(b => b.PhysiciansAddressId)
                     .HasConstraintName("FK_Patient_Physicians_Address");

                entity.HasOne(d => d.PrivateInsurance)
                     .WithOne(p => p.Patient)
                     .HasForeignKey<Patient>(b => b.PrivateInsuranceId)
                     .HasConstraintName("FK_Patient_PrivateInsurance_PrivateInsuranceId");

                entity.HasOne(d => d.Medicare)
                     .WithOne(p => p.Patient)
                     .HasForeignKey<Patient>(b => b.MedicareId)
                     .HasConstraintName("FK_Patient_Medicare_MedicareId");
            });
        }

        protected void IntakeFormBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntakeForm>(entity =>
            {
                entity.ToTable("IntakeForm", "dbo");

                entity.Property(e => e.IntakeFormType).IsRequired().HasMaxLength(100).HasConversion<string>();

                entity.Property(e => e.Status).IsRequired().HasMaxLength(100).HasConversion<string>().HasDefaultValue(IntakeFormStatus.New);

                entity.HasKey(e => e.IntakeFormId).ForSqlServerIsClustered(false);

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Patient)
                     .WithMany(p => p.IntakeForms)
                     .HasForeignKey(b => b.PatientId)
                     .HasConstraintName("FK_Patient_IntakeForms");

                entity.HasOne(d => d.Physician)
                     .WithMany(p => p.IntakeForms)
                     .HasForeignKey(b => b.PhysicianId)
                     .HasConstraintName("FK_Physician_IntakeForms");

                entity.HasOne(intake => intake.Document)
                     .WithOne(doc => doc.IntakeForm)
                     .HasForeignKey<IntakeForm>(intake => intake.DocumentId)
                     .HasConstraintName("FK_IntakeForm_Document");
            });
        }

        protected void DocumentBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document", "dbo");

                entity.HasKey(e => e.DocumentId).ForSqlServerIsClustered(false);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                //entity.HasOne(doc => doc.IntakeForm)
                //     .WithOne(intake => intake.Document)
                //     .HasForeignKey<Document>(doc => doc.IntakeFormId)
                //     .HasConstraintName("FK_IntakeForm_Document");
            });
        }

        protected void SignatureBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Signature>(entity =>
            {
                entity.ToTable("Signature", "dbo");

                entity.HasKey(e => e.SignatureId).ForSqlServerIsClustered(false);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");
            });
        }


        protected void ICD10CodeBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ICD10Code>(entity =>
            {
                entity.ToTable("ICD10Code", "dbo");

                entity.HasKey(e => e.ICD10CodeId).ForSqlServerIsClustered(false);

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.CreatedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("(getdate())");
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
                     .HasConstraintName("FK_Questions_Answers");
            });
        }

    }
}
