using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDispenceMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Dispenced",
                table: "Medicines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "DispensedMedicines",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Cost] * [Quantity]"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dispenced",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "DispensedMedicines");
        }
    }
}
