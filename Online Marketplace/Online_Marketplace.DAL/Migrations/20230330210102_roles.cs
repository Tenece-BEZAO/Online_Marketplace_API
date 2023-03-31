using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09b6d656-ab59-43a6-bf71-a3d37e2c2ce6", "81ac6f0d-a8d9-4f56-851b-31a4176b7190", "Admin", "ADMIN" },
                    { "26866c1f-96fc-4e09-945a-5b8127c9d501", "2626dae5-3ecc-4c86-9c95-c979288a8fda", "Seller", "SELLER" },
                    { "aa0c1a71-b7c9-46fa-8c27-6af984b7ecc0", "360fcc59-4861-4ac2-a350-bd6257e9778f", "Buyer", "BUYER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09b6d656-ab59-43a6-bf71-a3d37e2c2ce6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26866c1f-96fc-4e09-945a-5b8127c9d501");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa0c1a71-b7c9-46fa-8c27-6af984b7ecc0");
        }
    }
}
