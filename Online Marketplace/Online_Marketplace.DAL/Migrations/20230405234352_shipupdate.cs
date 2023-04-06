using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class shipupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimateDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingMethod = table.Column<int>(type: "int", nullable: false),
                    Policy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d05d956-1d23-4848-a837-9af5584ed26e", "7fe27add-34a5-4907-be83-77bb68ff7a37", "Seller", "SELLER" },
                    { "72838cc8-b8fc-4c1a-ac5d-3e8a2150beae", "4bbc5db0-570d-4021-b14e-9eb81b61d2a2", "Admin", "ADMIN" },
                    { "9aebb1df-2372-4f2b-8f91-953b9630088a", "09fe6497-e257-4d53-bcae-344f74e1b769", "Buyer", "BUYER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d05d956-1d23-4848-a837-9af5584ed26e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72838cc8-b8fc-4c1a-ac5d-3e8a2150beae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aebb1df-2372-4f2b-8f91-953b9630088a");

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
    }
}
