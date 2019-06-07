using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class reverseflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Documents",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Physician_Documents",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Signature",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_IntakeFormId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_PhysicianId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_SignatureId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "PhysicianId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "SignatureId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "Document");

            migrationBuilder.AddColumn<string>(
                name: "HCPCS",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ICD10",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysicianId",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "IntakeForm",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeForm_PhysicianId",
                schema: "dbo",
                table: "IntakeForm",
                column: "PhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_IntakeFormId",
                schema: "dbo",
                table: "Document",
                column: "IntakeFormId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "Document",
                column: "IntakeFormId",
                principalSchema: "dbo",
                principalTable: "IntakeForm",
                principalColumn: "IntakeFormId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_IntakeForms",
                schema: "dbo",
                table: "IntakeForm",
                column: "PhysicianId",
                principalSchema: "dbo",
                principalTable: "Physician",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Physician_IntakeForms",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropIndex(
                name: "IX_IntakeForm_PhysicianId",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropIndex(
                name: "IX_Document_IntakeFormId",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "HCPCS",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "ICD10",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "PhysicianId",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.AddColumn<int>(
                name: "PhysicianId",
                schema: "dbo",
                table: "Document",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignatureId",
                schema: "dbo",
                table: "Document",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "Document",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Document_IntakeFormId",
                schema: "dbo",
                table: "Document",
                column: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_PhysicianId",
                schema: "dbo",
                table: "Document",
                column: "PhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_SignatureId",
                schema: "dbo",
                table: "Document",
                column: "SignatureId",
                unique: true,
                filter: "[SignatureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_Documents",
                schema: "dbo",
                table: "Document",
                column: "IntakeFormId",
                principalSchema: "dbo",
                principalTable: "IntakeForm",
                principalColumn: "IntakeFormId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_Documents",
                schema: "dbo",
                table: "Document",
                column: "PhysicianId",
                principalSchema: "dbo",
                principalTable: "Physician",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Signature",
                schema: "dbo",
                table: "Document",
                column: "SignatureId",
                principalSchema: "dbo",
                principalTable: "Signature",
                principalColumn: "SignatureId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
