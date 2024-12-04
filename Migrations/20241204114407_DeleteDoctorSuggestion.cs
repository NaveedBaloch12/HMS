using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDoctorSuggestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_DoctorSuggestions_DoctorSuggestionId",
                table: "LabResults");

            migrationBuilder.DropTable(
                name: "DoctorSuggestions");

            migrationBuilder.DropIndex(
                name: "IX_LabResults_DoctorSuggestionId",
                table: "LabResults");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "SuggestedTests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "SuggestedTests");

            migrationBuilder.CreateTable(
                name: "DoctorSuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    SuggestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_DoctorSuggestionId",
                table: "LabResults",
                column: "DoctorSuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSuggestions_DoctorId",
                table: "DoctorSuggestions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSuggestions_PatientId",
                table: "DoctorSuggestions",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_DoctorSuggestions_DoctorSuggestionId",
                table: "LabResults",
                column: "DoctorSuggestionId",
                principalTable: "DoctorSuggestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
