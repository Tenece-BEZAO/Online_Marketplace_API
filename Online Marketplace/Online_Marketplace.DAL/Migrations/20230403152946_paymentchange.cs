using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class paymentchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f27544d-2481-40c0-8422-71c43d736efb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "828cbbea-6de6-4647-aeb6-1b10a40c431b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4254952-dd0c-4a59-bfdb-0f5e93cdfe44");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentGateway",
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
                    { "86cacb45-54a3-4105-b4b9-4ac89786ce64", "42879d53-ad0b-4049-aa27-24bf2cbe1cad", "Admin", "ADMIN" },
                    { "9b694ef6-db2e-41da-8811-a6a3ac2ae558", "f18a53f4-1a6f-48af-bd91-fbc11097fc33", "Seller", "SELLER" },
                    { "a6d21fc3-2ad0-40eb-baff-19b2a6eacc58", "7af8735c-22e5-4177-99f7-aff8afd6bffb", "Buyer", "BUYER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PaymentGateway",
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
                    { "0f27544d-2481-40c0-8422-71c43d736efb", "c8e7c0e6-c0f8-4151-b75d-fafdb982922f", "Seller", "SELLER" },
                    { "828cbbea-6de6-4647-aeb6-1b10a40c431b", "d529e69c-fe1d-4b6f-be77-21f26cfa3888", "Buyer", "BUYER" },
                    { "c4254952-dd0c-4a59-bfdb-0f5e93cdfe44", "8ae30c09-52b5-4f50-8479-dc662c88da59", "Admin", "ADMIN" }
                });
        }
    }
}
