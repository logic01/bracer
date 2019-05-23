using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class emails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                schema: "dbo",
                table: "UserAccount",
                maxLength: 100,
                nullable: false,
                defaultValue: "test@test.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                schema: "dbo",
                table: "UserAccount");
        }
    }
}
