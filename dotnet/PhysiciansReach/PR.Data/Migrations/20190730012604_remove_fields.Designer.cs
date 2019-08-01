﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PR.Constants.Enums;
using PR.Data.Models;

namespace PR.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190730012604_remove_fields")]
    partial class remove_fields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PR.Data.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLineOne")
                        .HasMaxLength(100);

                    b.Property<string>("AddressLineTwo")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("AddressId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Address","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Admin", b =>
                {
                    b.Property<int>("UserAccountId");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("UserAccountId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Admin","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Agent", b =>
                {
                    b.Property<int>("UserAccountId");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("VendorId");

                    b.HasKey("UserAccountId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("VendorId");

                    b.ToTable("Agent","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("AnswerId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Document", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IntakeFormId");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("DocumentId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Document","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.ICD10Code", b =>
                {
                    b.Property<int>("ICD10CodeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IntakeFormId");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("ICD10CodeId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("IntakeFormId");

                    b.ToTable("ICD10Code","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.IntakeForm", b =>
                {
                    b.Property<int>("IntakeFormId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("DeniedReason");

                    b.Property<int?>("DocumentId");

                    b.Property<string>("Duration");

                    b.Property<string>("HCPCSCode");

                    b.Property<string>("IntakeFormType")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("PatientId");

                    b.Property<int?>("PhysicianId");

                    b.Property<string>("PhysicianNotes");

                    b.Property<bool>("PhysicianPaid");

                    b.Property<string>("Product");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasDefaultValue("New");

                    b.Property<bool>("VendorBilled");

                    b.Property<bool>("VendorPaid");

                    b.HasKey("IntakeFormId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("DocumentId")
                        .IsUnique()
                        .HasFilter("[DocumentId] IS NOT NULL");

                    b.HasIndex("PatientId");

                    b.HasIndex("PhysicianId");

                    b.ToTable("IntakeForm","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("StackTrace")
                        .IsRequired();

                    b.HasKey("LogId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Log","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Medicare", b =>
                {
                    b.Property<int>("MedicareId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MemberId")
                        .HasMaxLength(100);

                    b.Property<string>("PatientGroup")
                        .HasMaxLength(100);

                    b.Property<string>("Pcn")
                        .HasMaxLength(100);

                    b.Property<string>("SecondaryCarrier")
                        .HasMaxLength(100);

                    b.Property<string>("SecondarySubscriberNumber")
                        .HasMaxLength(100);

                    b.Property<string>("SubscriberNumber")
                        .HasMaxLength(100);

                    b.HasKey("MedicareId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Medicare","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<int>("AgentId");

                    b.Property<string>("Allergies")
                        .HasMaxLength(500);

                    b.Property<string>("BestTimeToCallBack")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("HadBraceBefore");

                    b.Property<string>("Height")
                        .HasMaxLength(6);

                    b.Property<string>("Insurance")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDme")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("MainPainArea");

                    b.Property<int?>("MedicareId");

                    b.Property<string>("Medications")
                        .HasMaxLength(100);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Notes")
                        .HasMaxLength(100);

                    b.Property<string>("OtherProducts")
                        .HasMaxLength(100);

                    b.Property<string>("PainCream");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int?>("PhysiciansAddressId");

                    b.Property<string>("PhysiciansName")
                        .HasMaxLength(100);

                    b.Property<string>("PhysiciansPhoneNumber")
                        .HasMaxLength(10);

                    b.Property<int?>("PrivateInsuranceId");

                    b.Property<string>("SecondPainArea");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ShoeSize")
                        .HasMaxLength(4);

                    b.Property<string>("Therapy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Waist")
                        .HasMaxLength(3);

                    b.Property<string>("Weight")
                        .HasMaxLength(3);

                    b.HasKey("PatientId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("AgentId");

                    b.HasIndex("MedicareId")
                        .IsUnique()
                        .HasFilter("[MedicareId] IS NOT NULL");

                    b.HasIndex("PhysiciansAddressId")
                        .IsUnique()
                        .HasFilter("[PhysiciansAddressId] IS NOT NULL");

                    b.HasIndex("PrivateInsuranceId")
                        .IsUnique()
                        .HasFilter("[PrivateInsuranceId] IS NOT NULL");

                    b.ToTable("Patient","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Physician", b =>
                {
                    b.Property<int>("UserAccountId");

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("DEA")
                        .HasMaxLength(100);

                    b.Property<string>("FaxNumber")
                        .HasMaxLength(10);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("NPI")
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("UserAccountId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Physician","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.PrivateInsurance", b =>
                {
                    b.Property<int>("PrivateInsuranceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bin")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(30);

                    b.Property<string>("Group")
                        .HasMaxLength(100);

                    b.Property<string>("Insurance")
                        .HasMaxLength(100);

                    b.Property<string>("InsuranceId")
                        .HasMaxLength(100);

                    b.Property<string>("PCN")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(10);

                    b.Property<string>("State")
                        .HasMaxLength(2);

                    b.Property<string>("Street")
                        .HasMaxLength(100);

                    b.Property<string>("Zip")
                        .HasMaxLength(10);

                    b.HasKey("PrivateInsuranceId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("PrivateInsurance","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IntakeFormId");

                    b.Property<string>("Key");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("QuestionId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("IntakeFormId");

                    b.ToTable("Question","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Signature", b =>
                {
                    b.Property<int>("SignatureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("IntakeFormId");

                    b.Property<string>("IpAddress");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("Type");

                    b.HasKey("SignatureId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("IntakeFormId");

                    b.ToTable("Signature","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.UserAccount", b =>
                {
                    b.Property<int>("UserAccountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasDefaultValue("test@test.com");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("UserAccountId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("UserAccount","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ContactFirstName");

                    b.Property<string>("ContactLastName");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DoingBusinessAs");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("VendorId");

                    b.ToTable("Vendor");
                });

            modelBuilder.Entity("PR.Data.Models.Admin", b =>
                {
                    b.HasOne("PR.Data.Models.UserAccount", "UserAccount")
                        .WithOne("Admin")
                        .HasForeignKey("PR.Data.Models.Admin", "UserAccountId")
                        .HasConstraintName("FK_Admin_UserAccount")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PR.Data.Models.Agent", b =>
                {
                    b.HasOne("PR.Data.Models.UserAccount", "UserAccount")
                        .WithOne("Agent")
                        .HasForeignKey("PR.Data.Models.Agent", "UserAccountId")
                        .HasConstraintName("FK_Agent_UserAccount")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PR.Data.Models.Vendor", "Vendor")
                        .WithMany("Agent")
                        .HasForeignKey("VendorId")
                        .HasConstraintName("FK_Agent_Vendor")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PR.Data.Models.Answer", b =>
                {
                    b.HasOne("PR.Data.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("FK_Questions_Answers")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PR.Data.Models.ICD10Code", b =>
                {
                    b.HasOne("PR.Data.Models.IntakeForm", "IntakeForm")
                        .WithMany("ICD10Codes")
                        .HasForeignKey("IntakeFormId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PR.Data.Models.IntakeForm", b =>
                {
                    b.HasOne("PR.Data.Models.Document", "Document")
                        .WithOne("IntakeForm")
                        .HasForeignKey("PR.Data.Models.IntakeForm", "DocumentId")
                        .HasConstraintName("FK_IntakeForm_Document");

                    b.HasOne("PR.Data.Models.Patient", "Patient")
                        .WithMany("IntakeForms")
                        .HasForeignKey("PatientId")
                        .HasConstraintName("FK_Patient_IntakeForms")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PR.Data.Models.Physician", "Physician")
                        .WithMany("IntakeForms")
                        .HasForeignKey("PhysicianId")
                        .HasConstraintName("FK_Physician_IntakeForms");
                });

            modelBuilder.Entity("PR.Data.Models.Patient", b =>
                {
                    b.HasOne("PR.Data.Models.Address", "Address")
                        .WithOne("Patient")
                        .HasForeignKey("PR.Data.Models.Patient", "AddressId")
                        .HasConstraintName("FK_Patient_Address")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PR.Data.Models.Agent", "Agent")
                        .WithMany("Patients")
                        .HasForeignKey("AgentId")
                        .HasConstraintName("FK_Patient_Agent")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PR.Data.Models.Medicare", "Medicare")
                        .WithOne("Patient")
                        .HasForeignKey("PR.Data.Models.Patient", "MedicareId")
                        .HasConstraintName("FK_Patient_Medicare_MedicareId");

                    b.HasOne("PR.Data.Models.Address", "PhysiciansAddress")
                        .WithOne("PatientsPhysician")
                        .HasForeignKey("PR.Data.Models.Patient", "PhysiciansAddressId")
                        .HasConstraintName("FK_Patient_Physicians_Address");

                    b.HasOne("PR.Data.Models.PrivateInsurance", "PrivateInsurance")
                        .WithOne("Patient")
                        .HasForeignKey("PR.Data.Models.Patient", "PrivateInsuranceId")
                        .HasConstraintName("FK_Patient_PrivateInsurance_PrivateInsuranceId");
                });

            modelBuilder.Entity("PR.Data.Models.Physician", b =>
                {
                    b.HasOne("PR.Data.Models.Address", "Address")
                        .WithOne("Physician")
                        .HasForeignKey("PR.Data.Models.Physician", "AddressId")
                        .HasConstraintName("FK_Physician_Address")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PR.Data.Models.UserAccount", "UserAccount")
                        .WithOne("Physician")
                        .HasForeignKey("PR.Data.Models.Physician", "UserAccountId")
                        .HasConstraintName("FK_Physician_UserAccount")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PR.Data.Models.Question", b =>
                {
                    b.HasOne("PR.Data.Models.IntakeForm", "IntakeForm")
                        .WithMany("Questions")
                        .HasForeignKey("IntakeFormId")
                        .HasConstraintName("FK_IntakeForm_Questions")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PR.Data.Models.Signature", b =>
                {
                    b.HasOne("PR.Data.Models.IntakeForm", "IntakeForm")
                        .WithMany("Signatures")
                        .HasForeignKey("IntakeFormId");
                });
#pragma warning restore 612, 618
        }
    }
}
