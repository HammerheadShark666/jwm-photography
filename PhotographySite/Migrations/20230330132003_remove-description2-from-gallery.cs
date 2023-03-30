using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographySite.Migrations
{
    /// <inheritdoc />
    public partial class removedescription2fromgallery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description2",
                table: "Gallery");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "Gallery",
                type: "nvarchar(1000)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description2",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description2",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description2",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description2",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description2",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description2",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description2",
                value: null);
        }
    }
}
