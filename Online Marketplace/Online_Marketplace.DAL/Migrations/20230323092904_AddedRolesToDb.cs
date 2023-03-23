using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06fbda32-c80e-49da-b0bd-a8fd5c16f4bf", "3e391db4-88a0-47fb-b13a-e654a29de225", "Admin", "ADMIN" },
                    { "133f0578-cf5f-454c-a982-a024354e821e", "0a9b6694-c04d-46ed-9599-2b02d0cfe9de", "Buyer", "BUYER" },
                    { "f5be898a-46ce-40b6-a9e1-7e21e7a7aad5", "b0a06102-fd75-48e3-be51-84a2c718ccaf", "Seller", "SELLER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06fbda32-c80e-49da-b0bd-a8fd5c16f4bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "133f0578-cf5f-454c-a982-a024354e821e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5be898a-46ce-40b6-a9e1-7e21e7a7aad5");
        }
    }
}
