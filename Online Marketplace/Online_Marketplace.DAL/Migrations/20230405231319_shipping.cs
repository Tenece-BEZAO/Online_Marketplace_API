using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class shipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d5f5be6-58c6-4c70-93b9-5ead756e1330");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7385a12-3147-4223-b756-4a3f771c1765");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3ec6ff4-c9e1-4a3c-8ec0-775c87cd23bd");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimateDeliveryDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "shippingmethod",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3007b9f2-cef3-4f84-abf0-afa1b0e0f224", "2628af9f-f266-4397-8b17-56c26e59bede", "Seller", "SELLER" },
                    { "b8acd9c7-8725-4583-a7fc-1dcb0dd8eb17", "dd3ad36e-4345-4ab5-b170-15995a65c7ae", "Buyer", "BUYER" },
                    { "cc820053-4bdc-4fb3-93a7-b8dc9e5cbc62", "2007c235-19b6-46a5-8c13-bedc6d3f7eb0", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3007b9f2-cef3-4f84-abf0-afa1b0e0f224");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8acd9c7-8725-4583-a7fc-1dcb0dd8eb17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc820053-4bdc-4fb3-93a7-b8dc9e5cbc62");

            migrationBuilder.DropColumn(
                name: "EstimateDeliveryDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "shippingmethod",
                table: "Order");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d5f5be6-58c6-4c70-93b9-5ead756e1330", "ebcfcd8d-cd8e-487d-8cbe-2d3eb689f90a", "Buyer", "BUYER" },
                    { "b7385a12-3147-4223-b756-4a3f771c1765", "0881e61a-e551-4db9-8664-62b063f4254c", "Admin", "ADMIN" },
                    { "f3ec6ff4-c9e1-4a3c-8ec0-775c87cd23bd", "5bc28847-97e3-4b16-855a-a25e613dc4e5", "Seller", "SELLER" }
                });
        }
    }
}
