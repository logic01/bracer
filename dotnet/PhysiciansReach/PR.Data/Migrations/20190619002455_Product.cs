using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalDrNotes",
                schema: "dbo",
                table: "IntakeForm",
                newName: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicianNotes",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "PhysicianNotes",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.RenameColumn(
                name: "Product",
                schema: "dbo",
                table: "IntakeForm",
                newName: "AdditionalDrNotes");
        }
    }
}
