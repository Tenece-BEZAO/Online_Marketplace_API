using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Marketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "141936a7-9d6f-48ab-bd2b-ec0214b5831c", "c0ef1614-0558-456f-8d2a-0e1e864af2de", "Seller", "SELLER" },
                    { "8ce67a04-ad40-4803-aded-c7653b6e90c9", "5a026c9b-9bfc-4406-b3f2-16b117fec3e1", "Buyer", "BUYER" },
                    { "9e988f7e-7c54-4da1-9926-9d0a2c8885f8", "d01b06d0-f820-4ebe-b489-eeaf09073321", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "141936a7-9d6f-48ab-bd2b-ec0214b5831c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ce67a04-ad40-4803-aded-c7653b6e90c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e988f7e-7c54-4da1-9926-9d0a2c8885f8");
        }
    }
}
