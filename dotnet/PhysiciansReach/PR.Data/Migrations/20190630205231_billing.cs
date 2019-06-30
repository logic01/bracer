using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class billing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PhysicianBilled",
                schema: "dbo",
                table: "IntakeForm",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VendorBilled",
                schema: "dbo",
                table: "IntakeForm",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VendorPaid",
                schema: "dbo",
                table: "IntakeForm",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhysicianBilled",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "VendorBilled",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "VendorPaid",
                schema: "dbo",
                table: "IntakeForm");
        }
    }
}
