using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class billing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhysicianBilled",
                schema: "dbo",
                table: "IntakeForm",
                newName: "PhysicianPaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhysicianPaid",
                schema: "dbo",
                table: "IntakeForm",
                newName: "PhysicianBilled");
        }
    }
}
