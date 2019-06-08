using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class AddIntakeFormChildRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Patient_MedicareId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient");

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

            migrationBuilder.RenameTable(
                name: "PrivateInsurance",
                newName: "PrivateInsurance",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Medicare",
                newName: "Medicare",
                newSchema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "SignatureId",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true);

         
            migrationBuilder.CreateTable(
                name: "HCPCS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Product = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCPCS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ICD10",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD10", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_MedicareId",
                schema: "dbo",
                table: "Patient",
                column: "MedicareId",
                unique: true,
                filter: "[MedicareId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient",
                column: "PrivateInsuranceId",
                unique: true,
                filter: "[PrivateInsuranceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeForm_DocumentId",
                schema: "dbo",
                table: "IntakeForm",
                column: "DocumentId",
                unique: true,
                filter: "[DocumentId] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_IntakeForm_SignatureId",
                schema: "dbo",
                table: "IntakeForm",
                column: "SignatureId",
                unique: true,
                filter: "[SignatureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "IntakeForm",
                column: "DocumentId",
                principalSchema: "dbo",
                principalTable: "Document",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_Signature",
                schema: "dbo",
                table: "IntakeForm",
                column: "SignatureId",
                principalSchema: "dbo",
                principalTable: "Signature",
                principalColumn: "SignatureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_HCPCS",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_ICD10",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Signature",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropTable(
                name: "HCPCS");

            migrationBuilder.DropTable(
                name: "ICD10");

            migrationBuilder.DropIndex(
                name: "IX_Patient_MedicareId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_IntakeForm_DocumentId",
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

            migrationBuilder.DropIndex(
                name: "IX_IntakeForm_SignatureId",
                schema: "dbo",
                table: "IntakeForm");

           

            migrationBuilder.DropColumn(
                name: "DocumentId",
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

            migrationBuilder.DropColumn(
                name: "SignatureId",
                schema: "dbo",
                table: "IntakeForm");

          
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

         

            migrationBuilder.CreateIndex(
                name: "IX_Patient_MedicareId",
                schema: "dbo",
                table: "Patient",
                column: "MedicareId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PrivateInsuranceId",
                schema: "dbo",
                table: "Patient",
                column: "PrivateInsuranceId");

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
        }
    }
}
