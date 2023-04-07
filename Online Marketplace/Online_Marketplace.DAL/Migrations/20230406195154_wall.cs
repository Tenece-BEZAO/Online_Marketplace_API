using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class wall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30b5b666-b386-4261-8ce3-ea7f4ac814d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b01d7c0-38aa-49f4-a0bd-fcc657248b0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e59c7a6d-01db-4ad0-8d93-1b29c4cc3322");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e98ba27-a0d4-4d5e-8da9-e9be3b7678f2", "304c93fa-c4a2-4f4b-8912-91f1f461e5ba", "Admin", "ADMIN" },
                    { "3f2e37d7-5b00-41b9-83aa-2a55f2475b65", "b6db8fe7-e6f1-4223-9b72-9b2fe594c3d9", "Buyer", "BUYER" },
                    { "a032e714-dd99-46b1-9749-640a86433b8e", "f8512c10-bddc-4990-97ef-494efe6f1109", "Seller", "SELLER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e98ba27-a0d4-4d5e-8da9-e9be3b7678f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f2e37d7-5b00-41b9-83aa-2a55f2475b65");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a032e714-dd99-46b1-9749-640a86433b8e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30b5b666-b386-4261-8ce3-ea7f4ac814d3", "d0ea1aa4-f72b-4cae-ae46-13f24a0fef1f", "Buyer", "BUYER" },
                    { "6b01d7c0-38aa-49f4-a0bd-fcc657248b0c", "7edc6a6e-d415-417c-8c8e-6f9561f3e94b", "Seller", "SELLER" },
                    { "e59c7a6d-01db-4ad0-8d93-1b29c4cc3322", "e67118f3-0f34-4b9e-8f49-361b26e6be22", "Admin", "ADMIN" }
                });
        }
    }
}
