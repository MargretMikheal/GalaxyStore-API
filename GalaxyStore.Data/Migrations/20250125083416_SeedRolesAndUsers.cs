using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaxyStore.Data.Migrations
{
    public partial class SeedRolesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Roles
            migrationBuilder.InsertData(
                table: "Roles",
                schema: "Account",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "Admin", "ADMIN" },
                    { "2", "Manager", "MANAGER" },
                    { "3", "Sales", "SALES" }
                });

            // Insert Admin User
            var hasher = new PasswordHasher<IdentityUser>();
            var adminPassword = hasher.HashPassword(null, "Admin@123");

            migrationBuilder.InsertData(
                table: "Users",
                schema: "Account",
                columns: new[]
                {
                    "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed",
                    "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "AccessFailedCount",
                    "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled",
                    "EmployeeId", "Name", "Gander", "Password"
                },
                values: new object[]
                {
                    "admin-id", "admin@galaxystore.com", "ADMIN@GALAXYSTORE.COM", "admin@galaxystore.com",
                    "ADMIN@GALAXYSTORE.COM", true, adminPassword, string.Empty, string.Empty,
                    0, false, false, false, "E001", "Admin User", 0, "Admin@123"
                });

            // Assign Admin Role to Admin User
            migrationBuilder.InsertData(
                table: "UserRoles",
                schema: "Account",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "admin-id", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seeded data (reverses Up method)
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Account",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "admin-id", "1" });

            migrationBuilder.DeleteData(
                table: "Users",
                schema: "Account",
                keyColumn: "Id",
                keyValue: "admin-id");

            migrationBuilder.DeleteData(
                table: "Roles",
                schema: "Account",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Roles",
                schema: "Account",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Roles",
                schema: "Account",
                keyColumn: "Id",
                keyValue: "3");
        }
    }
}
