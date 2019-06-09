using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class AddExtraPatientProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                schema: "dbo",
                table: "Patient",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height",
                schema: "dbo",
                table: "Patient",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShoeSize",
                schema: "dbo",
                table: "Patient",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Waist",
                schema: "dbo",
                table: "Patient",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                schema: "dbo",
                table: "Patient",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "ShoeSize",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Waist",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "dbo",
                table: "Patient");
        }
    }
}
