using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class removecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HCPCSCode",
                schema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "HCPCSCode",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HCPCSCode",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.CreateTable(
                name: "HCPCSCode",
                schema: "dbo",
                columns: table => new
                {
                    HCPCSCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    IntakeFormId = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCPCSCode", x => x.HCPCSCodeId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_HCPCSCode_IntakeForm_IntakeFormId",
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "IntakeForm",
                        principalColumn: "IntakeFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HCPCSCode_IntakeFormId",
                schema: "dbo",
                table: "HCPCSCode",
                column: "IntakeFormId");
        }
    }
}
