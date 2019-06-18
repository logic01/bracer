using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class codes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Signature",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropTable(
                name: "HCPCS");

            migrationBuilder.DropTable(
                name: "ICD10");

            migrationBuilder.DropIndex(
                name: "IX_IntakeForm_SignatureId",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.AddColumn<int>(
                name: "IntakeFormId",
                schema: "dbo",
                table: "Signature",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "dbo",
                table: "Signature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalDrNotes",
                schema: "dbo",
                table: "IntakeForm",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "HCPCSCode",
                schema: "dbo",
                columns: table => new
                {
                    HCPCSCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntakeFormId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCPCSCode", x => x.HCPCSCodeId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_HCPCSCode_IntakeForm_IntakeFormId",
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "IntakeForm",
                        principalColumn: "IntakeFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ICD10Code",
                schema: "dbo",
                columns: table => new
                {
                    ICD10CodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntakeFormId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD10Code", x => x.ICD10CodeId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ICD10Code_IntakeForm_IntakeFormId",
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "IntakeForm",
                        principalColumn: "IntakeFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Signature_IntakeFormId",
                schema: "dbo",
                table: "Signature",
                column: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_HCPCSCode_IntakeFormId",
                schema: "dbo",
                table: "HCPCSCode",
                column: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ICD10Code_IntakeFormId",
                schema: "dbo",
                table: "ICD10Code",
                column: "IntakeFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signature_IntakeForm_IntakeFormId",
                schema: "dbo",
                table: "Signature",
                column: "IntakeFormId",
                principalSchema: "dbo",
                principalTable: "IntakeForm",
                principalColumn: "IntakeFormId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signature_IntakeForm_IntakeFormId",
                schema: "dbo",
                table: "Signature");

            migrationBuilder.DropTable(
                name: "HCPCSCode",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ICD10Code",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Signature_IntakeFormId",
                schema: "dbo",
                table: "Signature");

            migrationBuilder.DropColumn(
                name: "IntakeFormId",
                schema: "dbo",
                table: "Signature");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "dbo",
                table: "Signature");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalDrNotes",
                schema: "dbo",
                table: "IntakeForm",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "HCPCS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(maxLength: 100, nullable: true),
                    IntakeFormId = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Product = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCPCS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HCPCS_IntakeForm_IntakeFormId",
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "IntakeForm",
                        principalColumn: "IntakeFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ICD10",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IntakeFormId = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD10", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ICD10_IntakeForm_IntakeFormId",
                        column: x => x.IntakeFormId,
                        principalSchema: "dbo",
                        principalTable: "IntakeForm",
                        principalColumn: "IntakeFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntakeForm_SignatureId",
                schema: "dbo",
                table: "IntakeForm",
                column: "SignatureId",
                unique: true,
                filter: "[SignatureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HCPCS_IntakeFormId",
                table: "HCPCS",
                column: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ICD10_IntakeFormId",
                table: "ICD10",
                column: "IntakeFormId");

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
    }
}
