using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class AddLabTechnicianTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorSuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuggestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSuggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSuggestions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSuggestions_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorSuggestionId = table.Column<int>(type: "int", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabResults_DoctorSuggestions_DoctorSuggestionId",
                        column: x => x.DoctorSuggestionId,
                        principalTable: "DoctorSuggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabResults_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSuggestions_DoctorId",
                table: "DoctorSuggestions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSuggestions_PatientId",
                table: "DoctorSuggestions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_DoctorSuggestionId",
                table: "LabResults",
                column: "DoctorSuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_PatientId",
                table: "LabResults",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabResults");

            migrationBuilder.DropTable(
                name: "DoctorSuggestions");
        }
    }
}
