using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TotalControlEEAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessLocationRegisterType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "LastName", "Name", "Status" },
                values: new object[,]
                {
                    { 3, "F", "Martinez", "Luisa", "Unmodified" },
                    { 4, "F", "Alvarado", "Rosa", "Unmodified" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
