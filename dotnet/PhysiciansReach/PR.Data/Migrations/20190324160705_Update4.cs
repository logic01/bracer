using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Physician",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropIndex(
                name: "IX_Physician_UserAccountId",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_UserAccountId",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_UserAccountId",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "PhysicianId",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropColumn(
                name: "AgentId",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "AdminId",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Physician",
                schema: "dbo",
                table: "Physician",
                column: "UserAccountId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                schema: "dbo",
                table: "Agent",
                column: "UserAccountId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin",
                column: "UserAccountId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Physician",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.AddColumn<int>(
                name: "PhysicianId",
                schema: "dbo",
                table: "Physician",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                schema: "dbo",
                table: "Agent",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                schema: "dbo",
                table: "Admin",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Physician",
                schema: "dbo",
                table: "Physician",
                column: "PhysicianId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                schema: "dbo",
                table: "Agent",
                column: "AgentId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin",
                column: "AdminId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Physician_UserAccountId",
                schema: "dbo",
                table: "Physician",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agent_UserAccountId",
                schema: "dbo",
                table: "Agent",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserAccountId",
                schema: "dbo",
                table: "Admin",
                column: "UserAccountId",
                unique: true);
        }
    }
}
