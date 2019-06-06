using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class physician_newids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DEA",
                schema: "dbo",
                table: "Physician",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NPI",
                schema: "dbo",
                table: "Physician",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DEA",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropColumn(
                name: "NPI",
                schema: "dbo",
                table: "Physician");
        }
    }
}
