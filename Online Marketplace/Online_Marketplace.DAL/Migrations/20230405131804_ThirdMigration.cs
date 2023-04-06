using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId1",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductId1",
                table: "ProductReviews");

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

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductReviews");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductReviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductIdentity",
                table: "ProductReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9910ad3e-f6bd-48b8-9734-5b6f3b177f87", "d48d0146-0fb2-4a52-b821-5a1fa007a391", "Seller", "SELLER" },
                    { "a2bd779d-b3f3-41cd-816a-e72aa53090fb", "cd1e0e8b-2a94-4b08-b320-f76afe5f71d4", "Buyer", "BUYER" },
                    { "cab90bdc-e7fb-4aa8-b617-52ef360f377e", "21f68be5-0759-4a8f-a59f-ce329081cc75", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductIdentity",
                table: "ProductReviews",
                column: "ProductIdentity");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductIdentity",
                table: "ProductReviews",
                column: "ProductIdentity",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductIdentity",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductIdentity",
                table: "ProductReviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9910ad3e-f6bd-48b8-9734-5b6f3b177f87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2bd779d-b3f3-41cd-816a-e72aa53090fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cab90bdc-e7fb-4aa8-b617-52ef360f377e");

            migrationBuilder.DropColumn(
                name: "ProductIdentity",
                table: "ProductReviews");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductReviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductReviews",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0977f928-00c2-4eb0-9fd4-3034770fead5", "0a4646fd-5f88-457c-b37a-61efff683f11", "Admin", "ADMIN" },
                    { "3c77480d-4f92-49ce-8040-ef8c422c7d5e", "faeca0c9-a2d0-4342-b2ac-d4fc6860bd12", "Buyer", "BUYER" },
                    { "c32b5f4c-2981-4157-927d-9b24903540e1", "96d4a0d7-7356-4598-bf0f-42299f121bb7", "Seller", "SELLER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId1",
                table: "ProductReviews",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId1",
                table: "ProductReviews",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
