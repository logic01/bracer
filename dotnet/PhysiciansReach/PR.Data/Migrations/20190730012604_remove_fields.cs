using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class remove_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallBackImmediately",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Pharmacy",
                schema: "dbo",
                table: "Patient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CallBackImmediately",
                schema: "dbo",
                table: "Patient",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Pharmacy",
                schema: "dbo",
                table: "Patient",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
