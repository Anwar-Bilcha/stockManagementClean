using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class unitOfMeasureColumnAddedProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitOfMesaure",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMesaure",
                table: "Products");
        }
    }
}
