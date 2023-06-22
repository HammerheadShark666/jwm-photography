using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographySite.Migrations
{
    /// <inheritdoc />
    public partial class addusergallerytables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGalleryPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGalleryId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGalleryPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGalleryPhoto_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGalleryPhoto_UserGallery_UserGalleryId",
                        column: x => x.UserGalleryId,
                        principalTable: "UserGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGalleryPhoto_PhotoId",
                table: "UserGalleryPhoto",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGalleryPhoto_UserGalleryId",
                table: "UserGalleryPhoto",
                column: "UserGalleryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGalleryPhoto");

            migrationBuilder.DropTable(
                name: "UserGallery");
        }
    }
}
