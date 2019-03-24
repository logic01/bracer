using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Physician_PhysicianId",
                schema: "dbo",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_PhysicianId",
                schema: "dbo",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "PhysicianId",
                schema: "dbo",
                table: "UserAccount");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "dbo",
                table: "Physician",
                newName: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Physician_UserAccountId",
                schema: "dbo",
                table: "Physician",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_UserAccount",
                schema: "dbo",
                table: "Physician",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Physician_UserAccount",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropIndex(
                name: "IX_Physician_UserAccountId",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                schema: "dbo",
                table: "Physician",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "PhysicianId",
                schema: "dbo",
                table: "UserAccount",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_PhysicianId",
                schema: "dbo",
                table: "UserAccount",
                column: "PhysicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Physician_PhysicianId",
                schema: "dbo",
                table: "UserAccount",
                column: "PhysicianId",
                principalSchema: "dbo",
                principalTable: "Physician",
                principalColumn: "PhysicianId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
