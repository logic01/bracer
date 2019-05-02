using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class documentnophyscreq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhysicianId",
                schema: "dbo",
                table: "Document",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhysicianId",
                schema: "dbo",
                table: "Document",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
