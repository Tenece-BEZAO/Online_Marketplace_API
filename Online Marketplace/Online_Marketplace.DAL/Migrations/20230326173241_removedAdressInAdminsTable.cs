using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removedAdressInAdminsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "085f539d-d022-4e76-a7af-f6ea51681645");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b65ce7a-33a5-4fd2-bc7b-cd8eb76eaea8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f792a2dc-2930-4f30-a51a-b05bebaa7488");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Admins");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52fe4a2b-ede1-4252-98b5-092b9131d08f", "944e8560-2069-4821-8859-e61892d2eda7", "Seller", "SELLER" },
                    { "54b76c66-1a5b-48d1-bd84-6880431e46c0", "9d198ccb-3446-4473-a40a-1ddbf6c2f327", "Admin", "ADMIN" },
                    { "b13ed7c9-2c5a-41f2-a2b1-7c39eca4f029", "bd841a01-2aa7-45a9-a4f8-a912dc8353cd", "Buyer", "BUYER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52fe4a2b-ede1-4252-98b5-092b9131d08f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54b76c66-1a5b-48d1-bd84-6880431e46c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b13ed7c9-2c5a-41f2-a2b1-7c39eca4f029");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "085f539d-d022-4e76-a7af-f6ea51681645", "3ba519c5-b9c7-47c7-9b3c-2de0bd6cfae5", "Buyer", "BUYER" },
                    { "7b65ce7a-33a5-4fd2-bc7b-cd8eb76eaea8", "a16da008-3118-4523-a8b9-4c150e124183", "Seller", "SELLER" },
                    { "f792a2dc-2930-4f30-a51a-b05bebaa7488", "2f5bacf0-3d38-46aa-a6ab-cf2ed7c4a7cf", "Admin", "ADMIN" }
                });
        }
    }
}
