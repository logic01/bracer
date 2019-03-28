using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactFirstName",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropColumn(
                name: "ContactLastName",
                schema: "dbo",
                table: "Physician");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactFirstName",
                schema: "dbo",
                table: "Physician",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactLastName",
                schema: "dbo",
                table: "Physician",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
