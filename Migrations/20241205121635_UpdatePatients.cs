﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClearedBills",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClearedBills",
                table: "Patients");
        }
    }
}
