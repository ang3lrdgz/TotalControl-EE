using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotalControlEEAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    IdRegister = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    registerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    businessLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.IdRegister);
                    table.ForeignKey(
                        name: "FK_Registers_Employees_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateUpdate",
                value: new DateTime(2023, 3, 20, 2, 31, 11, 26, DateTimeKind.Local).AddTicks(5751));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateUpdate",
                value: new DateTime(2023, 3, 20, 2, 31, 11, 26, DateTimeKind.Local).AddTicks(5769));

            migrationBuilder.CreateIndex(
                name: "IX_Registers_IdEmployee",
                table: "Registers",
                column: "IdEmployee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registers");

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
                column: "DateUpdate",
                value: new DateTime(2023, 3, 20, 1, 4, 10, 148, DateTimeKind.Local).AddTicks(7566));
        }
    }
}
