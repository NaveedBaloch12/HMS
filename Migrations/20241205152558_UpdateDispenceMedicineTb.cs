using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDispenceMedicineTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DispensedMedicines_PharmacyInventories_MedicineId",
                table: "DispensedMedicines");

            migrationBuilder.DropIndex(
                name: "IX_DispensedMedicines_MedicineId",
                table: "DispensedMedicines");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "DispensedMedicines");

            migrationBuilder.AddColumn<int>(
                name: "PharmacyInventoryId",
                table: "DispensedMedicines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DispensedMedicines_PharmacyInventoryId",
                table: "DispensedMedicines",
                column: "PharmacyInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DispensedMedicines_PharmacyInventories_PharmacyInventoryId",
                table: "DispensedMedicines",
                column: "PharmacyInventoryId",
                principalTable: "PharmacyInventories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DispensedMedicines_PharmacyInventories_PharmacyInventoryId",
                table: "DispensedMedicines");

            migrationBuilder.DropIndex(
                name: "IX_DispensedMedicines_PharmacyInventoryId",
                table: "DispensedMedicines");

            migrationBuilder.DropColumn(
                name: "PharmacyInventoryId",
                table: "DispensedMedicines");

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "DispensedMedicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DispensedMedicines_MedicineId",
                table: "DispensedMedicines",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_DispensedMedicines_PharmacyInventories_MedicineId",
                table: "DispensedMedicines",
                column: "MedicineId",
                principalTable: "PharmacyInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
