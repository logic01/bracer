using Microsoft.EntityFrameworkCore.Migrations;

namespace PR.Data.Migrations
{
    public partial class nodelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_UserAccount",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Agent_UserAccount",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Vendor",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Answers",
                schema: "dbo",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_IntakeForms",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Address",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Agent",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropForeignKey(
                name: "FK_Physician_UserAccount",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Questions",
                schema: "dbo",
                table: "Question");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_UserAccount",
                schema: "dbo",
                table: "Admin",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_UserAccount",
                schema: "dbo",
                table: "Agent",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Vendor",
                schema: "dbo",
                table: "Agent",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Answers",
                schema: "dbo",
                table: "Answer",
                column: "QuestionId",
                principalSchema: "dbo",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "IntakeForm",
                column: "DocumentId",
                principalSchema: "dbo",
                principalTable: "Document",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_IntakeForms",
                schema: "dbo",
                table: "IntakeForm",
                column: "PatientId",
                principalSchema: "dbo",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Address",
                schema: "dbo",
                table: "Patient",
                column: "AddressId",
                principalSchema: "dbo",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Agent",
                schema: "dbo",
                table: "Patient",
                column: "AgentId",
                principalSchema: "dbo",
                principalTable: "Agent",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician",
                column: "AddressId",
                principalSchema: "dbo",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_UserAccount",
                schema: "dbo",
                table: "Physician",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_Questions",
                schema: "dbo",
                table: "Question",
                column: "IntakeFormId",
                principalSchema: "dbo",
                principalTable: "IntakeForm",
                principalColumn: "IntakeFormId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_UserAccount",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Agent_UserAccount",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Vendor",
                schema: "dbo",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Answers",
                schema: "dbo",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Document",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_IntakeForms",
                schema: "dbo",
                table: "IntakeForm");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Address",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Agent",
                schema: "dbo",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropForeignKey(
                name: "FK_Physician_UserAccount",
                schema: "dbo",
                table: "Physician");

            migrationBuilder.DropForeignKey(
                name: "FK_IntakeForm_Questions",
                schema: "dbo",
                table: "Question");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_UserAccount",
                schema: "dbo",
                table: "Admin",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_UserAccount",
                schema: "dbo",
                table: "Agent",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Vendor",
                schema: "dbo",
                table: "Agent",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Answers",
                schema: "dbo",
                table: "Answer",
                column: "QuestionId",
                principalSchema: "dbo",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Patient_IntakeForms",
                schema: "dbo",
                table: "IntakeForm",
                column: "PatientId",
                principalSchema: "dbo",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Address",
                schema: "dbo",
                table: "Patient",
                column: "AddressId",
                principalSchema: "dbo",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Agent",
                schema: "dbo",
                table: "Patient",
                column: "AgentId",
                principalSchema: "dbo",
                principalTable: "Agent",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_Address",
                schema: "dbo",
                table: "Physician",
                column: "AddressId",
                principalSchema: "dbo",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_UserAccount",
                schema: "dbo",
                table: "Physician",
                column: "UserAccountId",
                principalSchema: "dbo",
                principalTable: "UserAccount",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeForm_Questions",
                schema: "dbo",
                table: "Question",
                column: "IntakeFormId",
                principalSchema: "dbo",
                principalTable: "IntakeForm",
                principalColumn: "IntakeFormId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
