using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotalControlEEAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateUpdate",
                value: new DateTime(2023, 3, 20, 1, 4, 10, 148, DateTimeKind.Local).AddTicks(7545));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateUpdate", "Gender" },
                values: new object[] { new DateTime(2023, 3, 20, 1, 4, 10, 148, DateTimeKind.Local).AddTicks(7566), "M" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Gender",
                value: "F");
        }
    }
}
