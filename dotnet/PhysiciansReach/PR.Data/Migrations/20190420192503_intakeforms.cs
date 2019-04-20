using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class intakeforms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntakeForm",
                schema: "dbo",
                columns: table => new
                {
                    IntakeFormId = table.Column<int>(nullable: false),
                    PatientId = table.Column<string>(nullable: true),
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
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                schema: "dbo",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false),
                    IntakeFormId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_IntakeForm_Questions",
                        column: x => x.QuestionId,
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
                    AnswerId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 100, nullable: false),
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
                        column: x => x.AnswerId,
                        principalSchema: "dbo",
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Question",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IntakeForm",
                schema: "dbo");
        }
    }
}
