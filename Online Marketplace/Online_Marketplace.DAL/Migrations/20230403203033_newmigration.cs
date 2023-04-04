using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86cacb45-54a3-4105-b4b9-4ac89786ce64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b694ef6-db2e-41da-8811-a6a3ac2ae558");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6d21fc3-2ad0-40eb-baff-19b2a6eacc58");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionReference",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ee2d214-22a8-4d1a-b8b8-89f840a7c1b5", "b064e0e3-5cfb-4995-8673-6861a49dbcae", "Buyer", "BUYER" },
                    { "18107d84-8297-4b1e-90de-64cfab4139a4", "82f1b510-03d4-4ad0-bce1-cc6ccfc9a65b", "Seller", "SELLER" },
                    { "35734ba7-2715-458e-add3-1877e2e0d7a6", "310830fc-c300-4cea-b5a7-6163252b25f7", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ee2d214-22a8-4d1a-b8b8-89f840a7c1b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18107d84-8297-4b1e-90de-64cfab4139a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35734ba7-2715-458e-add3-1877e2e0d7a6");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionReference",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "86cacb45-54a3-4105-b4b9-4ac89786ce64", "42879d53-ad0b-4049-aa27-24bf2cbe1cad", "Admin", "ADMIN" },
                    { "9b694ef6-db2e-41da-8811-a6a3ac2ae558", "f18a53f4-1a6f-48af-bd91-fbc11097fc33", "Seller", "SELLER" },
                    { "a6d21fc3-2ad0-40eb-baff-19b2a6eacc58", "7af8735c-22e5-4177-99f7-aff8afd6bffb", "Buyer", "BUYER" }
                });
        }
    }
}
