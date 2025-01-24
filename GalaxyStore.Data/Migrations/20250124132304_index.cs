using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueCode",
                schema: "Galaxy",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Serial",
                schema: "Galaxy",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                schema: "Galaxy",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Serial",
                schema: "Galaxy",
                table: "Products",
                column: "Serial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Barcode",
                schema: "Galaxy",
                table: "Items",
                column: "Barcode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Serial",
                schema: "Galaxy",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Items_Barcode",
                schema: "Galaxy",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Barcode",
                schema: "Galaxy",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Serial",
                schema: "Galaxy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UniqueCode",
                schema: "Galaxy",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
