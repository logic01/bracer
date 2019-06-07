using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class remstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "dbo",
                table: "Document");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "dbo",
                table: "Document",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
