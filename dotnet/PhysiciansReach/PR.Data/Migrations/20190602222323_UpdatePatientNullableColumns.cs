using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class UpdatePatientNullableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_PhysiciansAddressId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.AlterColumn<int>(
                name: "PhysiciansAddressId",
                schema: "dbo",
                table: "Patient",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "OtherProducts",
                schema: "dbo",
                table: "Patient",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "dbo",
                table: "Patient",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Medications",
                schema: "dbo",
                table: "Patient",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PhysiciansAddressId",
                schema: "dbo",
                table: "Patient",
                column: "PhysiciansAddressId",
                unique: true,
                filter: "[PhysiciansAddressId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_PhysiciansAddressId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.AlterColumn<int>(
                name: "PhysiciansAddressId",
                schema: "dbo",
                table: "Patient",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherProducts",
                schema: "dbo",
                table: "Patient",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "dbo",
                table: "Patient",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medications",
                schema: "dbo",
                table: "Patient",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PhysiciansAddressId",
                schema: "dbo",
                table: "Patient",
                column: "PhysiciansAddressId",
                unique: true);
        }
    }
}
