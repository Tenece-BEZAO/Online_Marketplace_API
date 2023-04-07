using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3cf8759-3919-45d6-b988-3abc813ae2bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3a41584-6091-4cd8-8b90-2c73b95ed8b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8f86ffc-1fb9-4fc4-8bb5-c75b3020e0f6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3fe3ce21-aa56-4d57-baec-7eb7272dad75", "02e11705-3c50-4a13-97a7-2cc73a1f4a78", "Seller", "SELLER" },
                    { "8cdbf377-4d36-4325-a5e2-22a7c95b9a4c", "892c99ec-a2a1-444e-8634-8ab5e48395c5", "Buyer", "BUYER" },
                    { "b7ee188b-f314-4ad8-970d-e42e1cb4d4b0", "2842cc72-8823-4346-b708-4d8ba3b89d51", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fe3ce21-aa56-4d57-baec-7eb7272dad75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cdbf377-4d36-4325-a5e2-22a7c95b9a4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7ee188b-f314-4ad8-970d-e42e1cb4d4b0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d3cf8759-3919-45d6-b988-3abc813ae2bf", "9070400e-cc4e-4a08-9cfc-d98e72dca65d", "Buyer", "BUYER" },
                    { "f3a41584-6091-4cd8-8b90-2c73b95ed8b3", "d5f5f453-c5ef-42b1-a344-a66901152bf4", "Seller", "SELLER" },
                    { "f8f86ffc-1fb9-4fc4-8bb5-c75b3020e0f6", "b7229619-e88e-4712-8d90-f51edae09015", "Admin", "ADMIN" }
                });
        }
    }
}
