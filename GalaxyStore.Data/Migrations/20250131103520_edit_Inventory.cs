using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class edit_Inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfProduct",
                schema: "Galaxy",
                table: "Inventories",
                newName: "NumOfProductInStore");

            migrationBuilder.AddColumn<int>(
                name: "NumOfProductInStock",
                schema: "Galaxy",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfProductInStock",
                schema: "Galaxy",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "NumOfProductInStore",
                schema: "Galaxy",
                table: "Inventories",
                newName: "NumOfProduct");
        }
    }
}
