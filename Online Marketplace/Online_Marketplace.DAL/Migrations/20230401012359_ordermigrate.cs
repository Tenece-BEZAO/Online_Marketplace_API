using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ordermigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e059a32-2ab6-4580-8beb-dac6a7d34090");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74ae491d-febd-4a0b-a67a-dc91ee00c05b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "778a82c1-65b4-4631-af8e-43ce5f87fdbc");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Order");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e059a32-2ab6-4580-8beb-dac6a7d34090", "a67a1ef4-ad75-4347-9f3e-77f9c9c529c3", "Buyer", "BUYER" },
                    { "74ae491d-febd-4a0b-a67a-dc91ee00c05b", "c963aeb2-afb7-4abb-8ded-781f2cf80103", "Seller", "SELLER" },
                    { "778a82c1-65b4-4631-af8e-43ce5f87fdbc", "6f4a6f40-8d2d-4991-b2ba-19560f15f079", "Admin", "ADMIN" }
                });
        }
    }
}
