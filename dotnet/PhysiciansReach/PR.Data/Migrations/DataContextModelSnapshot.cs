﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PR.Data.Models;

namespace PR.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
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
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
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
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("VendorId");

                    b.HasKey("UserAccountId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("VendorId");

                    b.ToTable("Agent","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Physician", b =>
                {
                    b.Property<int>("UserAccountId");

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("UserAccountId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Physician","dbo");
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
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
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

                    b.ToTable("UserAccount","dbo");
                });

            modelBuilder.Entity("PR.Data.Models.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasConstraintName("FK_Admin_UserAccount");
                });

            modelBuilder.Entity("PR.Data.Models.Agent", b =>
                {
                    b.HasOne("PR.Data.Models.UserAccount", "UserAccount")
                        .WithOne("Agent")
                        .HasForeignKey("PR.Data.Models.Agent", "UserAccountId")
                        .HasConstraintName("FK_Agent_UserAccount");

                    b.HasOne("PR.Data.Models.Vendor", "Vendor")
                        .WithMany("Agent")
                        .HasForeignKey("VendorId")
                        .HasConstraintName("FK_Agent_Vendor");
                });

            modelBuilder.Entity("PR.Data.Models.Physician", b =>
                {
                    b.HasOne("PR.Data.Models.Address", "Address")
                        .WithOne("Physician")
                        .HasForeignKey("PR.Data.Models.Physician", "AddressId")
                        .HasConstraintName("FK_Physician_Address");

                    b.HasOne("PR.Data.Models.UserAccount", "UserAccount")
                        .WithOne("Physician")
                        .HasForeignKey("PR.Data.Models.Physician", "UserAccountId")
                        .HasConstraintName("FK_Physician_UserAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
