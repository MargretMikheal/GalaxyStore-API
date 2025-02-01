using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Edit_InvoicTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "Galaxy",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPay",
                schema: "Galaxy",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "Galaxy",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalPay",
                schema: "Galaxy",
                table: "Invoices");
        }
    }
}
