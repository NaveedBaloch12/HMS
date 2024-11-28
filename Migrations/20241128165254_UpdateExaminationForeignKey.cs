using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExaminationForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examination_Appointments_AppointmentId",
                table: "Examination");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Examination_ExaminationId",
                table: "Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Examination",
                table: "Examination");

            migrationBuilder.RenameTable(
                name: "Examination",
                newName: "Examinations");

            migrationBuilder.RenameIndex(
                name: "IX_Examination_AppointmentId",
                table: "Examinations",
                newName: "IX_Examinations_AppointmentId");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Examinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Examinations",
                table: "Examinations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_PatientId",
                table: "Examinations",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Appointments_AppointmentId",
                table: "Examinations",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Patients_PatientId",
                table: "Examinations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Examinations_ExaminationId",
                table: "Medicines",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Appointments_AppointmentId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Patients_PatientId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Examinations_ExaminationId",
                table: "Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Examinations",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_PatientId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Examinations");

            migrationBuilder.RenameTable(
                name: "Examinations",
                newName: "Examination");

            migrationBuilder.RenameIndex(
                name: "IX_Examinations_AppointmentId",
                table: "Examination",
                newName: "IX_Examination_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Examination",
                table: "Examination",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Examination_Appointments_AppointmentId",
                table: "Examination",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Examination_ExaminationId",
                table: "Medicines",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
