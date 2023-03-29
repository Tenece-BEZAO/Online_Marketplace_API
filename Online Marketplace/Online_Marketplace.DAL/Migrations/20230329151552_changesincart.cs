using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changesincart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ce4bc67-c7f2-4161-832a-06a7ec32a2a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf03adbb-6ad7-4ae8-83ca-902d895db331");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8fa28b4-a482-475a-b6dd-4da80399b4d1");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "524f01c7-5f40-4e8e-96c0-2e4fbbca1bb8", "a5aa1006-7a23-485c-b1aa-ddecdfd333e6", "Seller", "SELLER" },
                    { "a0b79477-e8f0-49a9-a1e4-07aeeb2d7681", "a8023809-1562-40ce-8aec-ca6eb8049e94", "Admin", "ADMIN" },
                    { "d41c5f37-05b1-40d9-88ea-870b6dc630dc", "57a09e38-37e2-47c0-ab8b-b2dba6558bf0", "Buyer", "BUYER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BuyerId",
                table: "Carts",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Buyers_BuyerId",
                table: "Carts",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Buyers_BuyerId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_BuyerId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "524f01c7-5f40-4e8e-96c0-2e4fbbca1bb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0b79477-e8f0-49a9-a1e4-07aeeb2d7681");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d41c5f37-05b1-40d9-88ea-870b6dc630dc");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ce4bc67-c7f2-4161-832a-06a7ec32a2a8", "aa923ee4-d2d4-4715-a2f3-19cab08e3259", "Seller", "SELLER" },
                    { "bf03adbb-6ad7-4ae8-83ca-902d895db331", "ad52a97a-2292-4ac2-ad76-208123254749", "Admin", "ADMIN" },
                    { "c8fa28b4-a482-475a-b6dd-4da80399b4d1", "6e879877-7d9f-4797-8ac4-fec8250c1cea", "Buyer", "BUYER" }
                });
        }
    }
}
