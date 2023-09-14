using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Items.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "InventoryCount", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "color: sea", "/images/hsh-sea.jpg", 10, "Fender hsh", 1122m },
                    { 2, "color: flame top", "/images/hss-ft.jpg", 10, "Fender hss", 850m },
                    { 3, "color: black", "/images/sss-lf.jpg", 10, "Fender sss left", 850m },
                    { 4, "color: yellow", "/images/sss-yl.jpg", 10, "Fender sss", 850m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
