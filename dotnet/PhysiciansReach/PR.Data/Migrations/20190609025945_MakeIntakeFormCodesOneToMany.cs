using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class MakeIntakeFormCodesOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_HCPCS",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_ICD10",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropIndex(
                name: "IX_IntakeForm_HCPCSId",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropIndex(
                name: "IX_IntakeForm_ICD10Id",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "HCPCSId",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "ICD10Id",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.AddColumn<int>(
                name: "IntakeFormId",
                table: "ICD10",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntakeFormId",
                table: "HCPCS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ICD10_IntakeFormId",
                table: "ICD10",
                column: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_HCPCS_IntakeFormId",
                table: "HCPCS",
                column: "IntakeFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_HCPCS_IntakeForm_IntakeFormId",
                table: "HCPCS",
                column: "IntakeFormId",
                principalSchema: "dbo",
                principalTable: "IntakeForm",
                principalColumn: "IntakeFormId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ICD10_IntakeForm_IntakeFormId",
                table: "ICD10",
                column: "IntakeFormId",
                principalSchema: "dbo",
                principalTable: "IntakeForm",
                principalColumn: "IntakeFormId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HCPCS_IntakeForm_IntakeFormId",
                table: "HCPCS");

            migrationBuilder.DropForeignKey(
                name: "FK_ICD10_IntakeForm_IntakeFormId",
                table: "ICD10");

            migrationBuilder.DropIndex(
                name: "IX_ICD10_IntakeFormId",
                table: "ICD10");

            migrationBuilder.DropIndex(
                name: "IX_HCPCS_IntakeFormId",
                table: "HCPCS");

            migrationBuilder.DropColumn(
                name: "IntakeFormId",
                table: "ICD10");

            migrationBuilder.DropColumn(
                name: "IntakeFormId",
                table: "HCPCS");

            migrationBuilder.AddColumn<int>(
                name: "HCPCSId",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ICD10Id",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntakeForm_HCPCSId",
                schema: "dbo",
                table: "IntakeForm",
                column: "HCPCSId",
                unique: true,
                filter: "[HCPCSId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeForm_ICD10Id",
                schema: "dbo",
                table: "IntakeForm",
                column: "ICD10Id",
                unique: true,
                filter: "[ICD10Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_HCPCS",
                schema: "dbo",
                table: "IntakeForm",
                column: "HCPCSId",
                principalTable: "HCPCS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_ICD10",
                schema: "dbo",
                table: "IntakeForm",
                column: "ICD10Id",
                principalTable: "ICD10",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
