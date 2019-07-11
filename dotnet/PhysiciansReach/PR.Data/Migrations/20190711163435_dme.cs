using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class dme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HadBraceBefore",
                schema: "dbo",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainPainArea",
                schema: "dbo",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PainCream",
                schema: "dbo",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPainArea",
                schema: "dbo",
                table: "Patient",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HadBraceBefore",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "MainPainArea",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PainCream",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "SecondPainArea",
                schema: "dbo",
                table: "Patient");
        }
    }
}
