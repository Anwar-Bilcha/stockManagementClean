using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LogTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "StockManagementLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogException = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockManagementLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockManagementLogs");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpiryDate",
                table: "Products",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
