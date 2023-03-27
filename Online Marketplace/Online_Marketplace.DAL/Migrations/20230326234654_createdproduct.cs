using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createdproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43d02261-a271-4439-adff-97eb53c3c3b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61a2bc2c-a727-41cf-bff7-e19ae6df2c5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e38a3e7a-56b8-47ce-b756-6b5fb23ea0b7");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "554e9017-9000-4127-afb9-46797c5d6f8b", "21852779-bec4-4a92-9d67-0da27b2c4890", "Admin", "ADMIN" },
                    { "62456111-f1fb-4359-9454-9c7a26273a15", "2808262a-400d-449d-952e-782670590fc8", "Seller", "SELLER" },
                    { "b1dc80bf-c534-417a-802c-b7b8de31fd34", "cd841933-d368-48bd-b19b-2adba11f89c7", "Buyer", "BUYER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "554e9017-9000-4127-afb9-46797c5d6f8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62456111-f1fb-4359-9454-9c7a26273a15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1dc80bf-c534-417a-802c-b7b8de31fd34");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43d02261-a271-4439-adff-97eb53c3c3b3", "d526ccbf-9b9e-44fe-8119-383c9a338f79", "Buyer", "BUYER" },
                    { "61a2bc2c-a727-41cf-bff7-e19ae6df2c5c", "a2f2c4e9-3bd2-46e3-b593-d02fbb2e3c0b", "Seller", "SELLER" },
                    { "e38a3e7a-56b8-47ce-b756-6b5fb23ea0b7", "d228d787-4fd7-4f12-8035-42bb5bdc3aa6", "Admin", "ADMIN" }
                });
        }
    }
}
