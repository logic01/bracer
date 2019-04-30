using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document",
                schema: "dbo");
        }
    }
}
