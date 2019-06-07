using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class defstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "IntakeForm",
                maxLength: 100,
                nullable: false,
                defaultValue: "New",
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "IntakeForm",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldDefaultValue: "New");
        }
    }
}
