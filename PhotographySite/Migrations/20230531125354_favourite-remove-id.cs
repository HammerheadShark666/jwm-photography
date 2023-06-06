using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographySite.Migrations
{
    /// <inheritdoc />
    public partial class favouriteremoveid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favourite");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Favourite",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Favourite",
                type: "uuids",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Favourite",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
