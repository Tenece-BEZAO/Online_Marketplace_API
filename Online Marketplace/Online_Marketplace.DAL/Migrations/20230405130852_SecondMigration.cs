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
                keyValue: "226fbbc2-a9f1-4e58-ac71-b42b66d7ca69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30d6e3b0-6994-4379-a2ee-b63884bf1f36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60217e2b-5af1-4705-862f-f14d8d2b9542");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0977f928-00c2-4eb0-9fd4-3034770fead5", "0a4646fd-5f88-457c-b37a-61efff683f11", "Admin", "ADMIN" },
                    { "3c77480d-4f92-49ce-8040-ef8c422c7d5e", "faeca0c9-a2d0-4342-b2ac-d4fc6860bd12", "Buyer", "BUYER" },
                    { "c32b5f4c-2981-4157-927d-9b24903540e1", "96d4a0d7-7356-4598-bf0f-42299f121bb7", "Seller", "SELLER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0977f928-00c2-4eb0-9fd4-3034770fead5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c77480d-4f92-49ce-8040-ef8c422c7d5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c32b5f4c-2981-4157-927d-9b24903540e1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "226fbbc2-a9f1-4e58-ac71-b42b66d7ca69", "deb1388e-e9e9-478c-8c1d-b30fd26eb292", "Buyer", "BUYER" },
                    { "30d6e3b0-6994-4379-a2ee-b63884bf1f36", "14a2f844-83d4-49e2-bf35-a460492ff385", "Seller", "SELLER" },
                    { "60217e2b-5af1-4705-862f-f14d8d2b9542", "c6e572ce-12d4-459d-bde8-d58f51786304", "Admin", "ADMIN" }
                });
        }
    }
}
