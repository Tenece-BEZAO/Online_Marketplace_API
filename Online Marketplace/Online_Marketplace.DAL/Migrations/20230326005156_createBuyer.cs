using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createBuyer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fe49d0b-15c3-457e-8483-bc44cf038b09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b81caa0-6336-4af5-a469-4d68fa6badf8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df4c46d5-0494-429c-9996-8f3236d211df");

            migrationBuilder.CreateTable(
                name: "Buyers",
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
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buyers_AspNetUsers_UserId",
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
                    { "53db148c-45bf-43fd-9d43-81cf25e3c922", "3dd5d0b7-43f5-4efe-8894-cc78b37851b3", "Admin", "ADMIN" },
                    { "90a96181-03fb-4b13-99fa-07ba2f5ea11b", "57219852-de1a-4d27-a3d3-4b9e6f1af878", "Seller", "SELLER" },
                    { "a511a8c6-803c-424c-bc02-fa0f46f206fc", "e3c4737f-3e62-4fa1-b6d0-6dc034e88081", "Buyer", "BUYER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_UserId",
                table: "Buyers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buyers");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fe49d0b-15c3-457e-8483-bc44cf038b09", "d70ee6a0-f37a-405e-88ce-555612663458", "Buyer", "BUYER" },
                    { "4b81caa0-6336-4af5-a469-4d68fa6badf8", "4bfa4800-d558-4802-8c95-327824d46f64", "Seller", "SELLER" },
                    { "df4c46d5-0494-429c-9996-8f3236d211df", "d8b137fa-911a-409c-857c-b49e9eac07a0", "Admin", "ADMIN" }
                });
        }
    }
}
