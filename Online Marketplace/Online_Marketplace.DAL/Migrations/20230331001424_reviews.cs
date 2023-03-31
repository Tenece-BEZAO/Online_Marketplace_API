using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f56dd99-265a-4ef9-9aab-af39eeb95173", "1d4e299f-ae55-43e6-b528-13081d596ac8", "Seller", "SELLER" },
                    { "94994e21-0bbf-43c8-90eb-5c0a7a0d5727", "7349b9a3-803b-4154-8868-13d74cda7eb6", "Admin", "ADMIN" },
                    { "ff8e0c96-6c14-434c-8584-fc9151e42e57", "5dd534f7-7691-4343-8e10-d95f81e69647", "Buyer", "BUYER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_BuyerId",
                table: "ProductReviews",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f56dd99-265a-4ef9-9aab-af39eeb95173");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94994e21-0bbf-43c8-90eb-5c0a7a0d5727");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff8e0c96-6c14-434c-8584-fc9151e42e57");

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
    }
}
