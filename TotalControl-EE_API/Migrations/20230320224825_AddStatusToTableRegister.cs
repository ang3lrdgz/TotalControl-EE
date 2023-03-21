using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotalControlEEAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToTableRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Registers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Registers");
        }
    }
}
