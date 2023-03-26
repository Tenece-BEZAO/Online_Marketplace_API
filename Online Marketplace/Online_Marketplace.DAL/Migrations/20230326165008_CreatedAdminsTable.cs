using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAdminsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53db148c-45bf-43fd-9d43-81cf25e3c922");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90a96181-03fb-4b13-99fa-07ba2f5ea11b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a511a8c6-803c-424c-bc02-fa0f46f206fc");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSeller = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "085f539d-d022-4e76-a7af-f6ea51681645", "3ba519c5-b9c7-47c7-9b3c-2de0bd6cfae5", "Buyer", "BUYER" },
                    { "7b65ce7a-33a5-4fd2-bc7b-cd8eb76eaea8", "a16da008-3118-4523-a8b9-4c150e124183", "Seller", "SELLER" },
                    { "f792a2dc-2930-4f30-a51a-b05bebaa7488", "2f5bacf0-3d38-46aa-a6ab-cf2ed7c4a7cf", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53db148c-45bf-43fd-9d43-81cf25e3c922", "3dd5d0b7-43f5-4efe-8894-cc78b37851b3", "Admin", "ADMIN" },
                    { "90a96181-03fb-4b13-99fa-07ba2f5ea11b", "57219852-de1a-4d27-a3d3-4b9e6f1af878", "Seller", "SELLER" },
                    { "a511a8c6-803c-424c-bc02-fa0f46f206fc", "e3c4737f-3e62-4fa1-b6d0-6dc034e88081", "Buyer", "BUYER" }
                });
        }
    }
}
