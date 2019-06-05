using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class signature_seperate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                schema: "dbo",
                table: "Document");

            migrationBuilder.AddColumn<int>(
                name: "SignatureId",
                schema: "dbo",
                table: "Document",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Signature",
                schema: "dbo",
                columns: table => new
                {
                    SignatureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<byte[]>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signature", x => x.SignatureId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_SignatureId",
                schema: "dbo",
                table: "Document",
                column: "SignatureId",
                unique: true,
                filter: "[SignatureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Signature",
                schema: "dbo",
                table: "Document",
                column: "SignatureId",
                principalSchema: "dbo",
                principalTable: "Signature",
                principalColumn: "SignatureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Signature",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropTable(
                name: "Signature",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Document_SignatureId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "SignatureId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.AddColumn<byte[]>(
                name: "Signature",
                schema: "dbo",
                table: "Document",
                nullable: true);
        }
    }
}
