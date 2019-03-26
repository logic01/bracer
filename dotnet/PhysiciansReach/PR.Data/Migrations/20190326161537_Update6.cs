using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class Update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.RenameColumn(
                name: "AddresstId",
                schema: "dbo",
                table: "Physician",
                newName: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Physician_AddressId",
                schema: "dbo",
                table: "Physician",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician",
                column: "AddressId",
                principalSchema: "dbo",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropIndex(
                name: "IX_Physician_AddressId",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "dbo",
                table: "Physician",
                newName: "AddresstId");

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
