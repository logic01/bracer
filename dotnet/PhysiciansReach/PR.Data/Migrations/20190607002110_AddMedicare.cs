using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class AddMedicare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicareId",
                schema: "dbo",
                table: "Patient",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medicare",
                columns: table => new
                {
                    MedicareId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<string>(maxLength: 100, nullable: true),
                    PatientGroup = table.Column<string>(maxLength: 100, nullable: true),
                    Pcn = table.Column<string>(maxLength: 100, nullable: true),
                    SubscriberNumber = table.Column<string>(maxLength: 100, nullable: true),
                    SecondaryCarrier = table.Column<string>(maxLength: 100, nullable: true),
                    SecondarySubscriberNumber = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicare", x => x.MedicareId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_MedicareId",
                schema: "dbo",
                table: "Patient",
                column: "MedicareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Medicare_MedicareId",
                schema: "dbo",
                table: "Patient",
                column: "MedicareId",
                principalTable: "Medicare",
                principalColumn: "MedicareId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Medicare_MedicareId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropTable(
                name: "Medicare");

            migrationBuilder.DropIndex(
                name: "IX_Patient_MedicareId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "MedicareId",
                schema: "dbo",
                table: "Patient");
        }
    }
}
