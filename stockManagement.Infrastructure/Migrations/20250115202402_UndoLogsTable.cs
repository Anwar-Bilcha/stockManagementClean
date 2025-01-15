using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UndoLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockManagementLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockManagementLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogException = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockManagementLogs", x => x.Id);
                });
        }
    }
}
