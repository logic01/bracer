using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    DoingBusinessAs = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ContactFirstName = table.Column<string>(nullable: true),
                    ContactLastName = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLineOne = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLineTwo = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    State = table.Column<string>(maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                schema: "dbo",
                columns: table => new
                {
                    LogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Severity = table.Column<string>(maxLength: 100, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    StackTrace = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                schema: "dbo",
                columns: table => new
                {
                    UserAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<byte[]>(maxLength: 200, nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserAccountId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                schema: "dbo",
                columns: table => new
                {
                    UserAccountId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.UserAccountId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Admin_UserAccount",
                        column: x => x.UserAccountId,
                        principalSchema: "dbo",
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                schema: "dbo",
                columns: table => new
                {
                    UserAccountId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.UserAccountId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Agent_UserAccount",
                        column: x => x.UserAccountId,
                        principalSchema: "dbo",
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agent_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Physician",
                schema: "dbo",
                columns: table => new
                {
                    UserAccountId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 10, nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physician", x => x.UserAccountId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Physician_Address",
                        column: x => x.AddressId,
                        principalSchema: "dbo",
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Physician_UserAccount",
                        column: x => x.UserAccountId,
                        principalSchema: "dbo",
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                schema: "dbo",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentId = table.Column<int>(nullable: false),
                    Language = table.Column<string>(maxLength: 100, nullable: false),
                    Sex = table.Column<string>(maxLength: 100, nullable: false),
                    Therapy = table.Column<string>(maxLength: 100, nullable: false),
                    Insurance = table.Column<string>(maxLength: 100, nullable: false),
                    Pharmacy = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 10, nullable: false),
                    CallBackImmediately = table.Column<bool>(nullable: false, defaultValue: true),
                    BestTimeToCallBack = table.Column<string>(maxLength: 100, nullable: false),
                    IsDme = table.Column<bool>(nullable: false, defaultValue: false),
                    Medications = table.Column<string>(maxLength: 100, nullable: false),
                    Notes = table.Column<string>(maxLength: 100, nullable: false),
                    OtherProducts = table.Column<string>(maxLength: 100, nullable: false),
                    PhysiciansName = table.Column<string>(maxLength: 10, nullable: false),
                    PhysiciansPhoneNumber = table.Column<string>(maxLength: 10, nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    PhysiciansAddressId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Patient_Address",
                        column: x => x.AddressId,
                        principalSchema: "dbo",
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Agent",
                        column: x => x.AgentId,
                        principalSchema: "dbo",
                        principalTable: "Agent",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Physicians_Address",
                        column: x => x.PhysiciansAddressId,
                        principalSchema: "dbo",
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntakeForm",
                schema: "dbo",
                columns: table => new
                {
                    IntakeFormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(nullable: false),
                    IntakeFormType = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeForm", x => x.IntakeFormId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Patient_IntakeForms",
                        column: x => x.PatientId,
                        principalSchema: "dbo",
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "dbo",
                columns: table => new
                {
                    DocumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntakeFormId = table.Column<int>(nullable: false),
                    PhysicianId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: false),
                    Content = table.Column<byte[]>(nullable: false),
                    Signature = table.Column<byte[]>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_IntakeForm_Documents",
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "IntakeForm",
                        principalColumn: "IntakeFormId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Physician_Documents",
                        column: x => x.PhysicianId,
                        principalSchema: "dbo",
                        principalTable: "Physician",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                schema: "dbo",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntakeFormId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_IntakeForm_Questions",
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "IntakeForm",
                        principalColumn: "IntakeFormId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                schema: "dbo",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Questions_Answers",
                        column: x => x.QuestionId,
                        principalSchema: "dbo",
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_VendorId",
                schema: "dbo",
                table: "Agent",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                schema: "dbo",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_IntakeFormId",
                schema: "dbo",
                table: "Document",
                column: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_PhysicianId",
                schema: "dbo",
                table: "Document",
                column: "PhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeForm_PatientId",
                schema: "dbo",
                table: "IntakeForm",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_AddressId",
                schema: "dbo",
                table: "Patient",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_AgentId",
                schema: "dbo",
                table: "Patient",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PhysiciansAddressId",
                schema: "dbo",
                table: "Patient",
                column: "PhysiciansAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Physician_AddressId",
                schema: "dbo",
                table: "Physician",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_IntakeFormId",
                schema: "dbo",
                table: "Question",
                column: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_UserName",
                schema: "dbo",
                table: "UserAccount",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Answer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Log",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Question",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Physician",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IntakeForm",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Patient",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Agent",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserAccount",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
