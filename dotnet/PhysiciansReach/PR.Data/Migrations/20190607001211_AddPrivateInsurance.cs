using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class AddPrivateInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivateInsuranceId",
                schema: "dbo",
                table: "Patient",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrivateInsurance",
                columns: table => new
                {
                    PrivateInsuranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Insurance = table.Column<string>(maxLength: 100, nullable: true),
                    InsuranceId = table.Column<string>(maxLength: 100, nullable: true),
                    Group = table.Column<string>(maxLength: 100, nullable: true),
                    PCN = table.Column<string>(maxLength: 100, nullable: true),
                    Bin = table.Column<string>(maxLength: 100, nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 30, nullable: true),
                    State = table.Column<string>(maxLength: 2, nullable: true),
                    Zip = table.Column<string>(maxLength: 10, nullable: true),
                    Phone = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateInsurance", x => x.PrivateInsuranceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient",
                column: "PrivateInsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_PrivateInsurance_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient",
                column: "PrivateInsuranceId",
                principalTable: "PrivateInsurance",
                principalColumn: "PrivateInsuranceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_PrivateInsurance_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropTable(
                name: "PrivateInsurance");

            migrationBuilder.DropIndex(
                name: "IX_Patient_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PrivateInsuranceId",
                schema: "dbo",
                table: "Patient");
        }
    }
}
