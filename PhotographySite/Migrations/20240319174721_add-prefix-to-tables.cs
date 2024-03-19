using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographySite.Migrations
{
    /// <inheritdoc />
    public partial class addprefixtotables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGalleryPhoto_PHOTO_Photo_PhotoId",
                table: "UserGalleryPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGalleryPhoto_UserGallery_UserGalleryId",
                table: "UserGalleryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGalleryPhoto",
                table: "UserGalleryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGallery",
                table: "UserGallery");

            migrationBuilder.RenameTable(
                name: "UserGalleryPhoto",
                newName: "PHOTO_UserGalleryPhoto");

            migrationBuilder.RenameTable(
                name: "UserGallery",
                newName: "PHOTO_UserGallery");

            migrationBuilder.RenameIndex(
                name: "IX_UserGalleryPhoto_UserGalleryId",
                table: "PHOTO_UserGalleryPhoto",
                newName: "IX_PHOTO_UserGalleryPhoto_UserGalleryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGalleryPhoto_PhotoId",
                table: "PHOTO_UserGalleryPhoto",
                newName: "IX_PHOTO_UserGalleryPhoto_PhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PHOTO_UserGalleryPhoto",
                table: "PHOTO_UserGalleryPhoto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PHOTO_UserGallery",
                table: "PHOTO_UserGallery",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PHOTO_UserGalleryPhoto_PHOTO_Photo_PhotoId",
                table: "PHOTO_UserGalleryPhoto",
                column: "PhotoId",
                principalTable: "PHOTO_Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PHOTO_UserGalleryPhoto_PHOTO_UserGallery_UserGalleryId",
                table: "PHOTO_UserGalleryPhoto",
                column: "UserGalleryId",
                principalTable: "PHOTO_UserGallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PHOTO_UserGalleryPhoto_PHOTO_Photo_PhotoId",
                table: "PHOTO_UserGalleryPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_PHOTO_UserGalleryPhoto_PHOTO_UserGallery_UserGalleryId",
                table: "PHOTO_UserGalleryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PHOTO_UserGalleryPhoto",
                table: "PHOTO_UserGalleryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PHOTO_UserGallery",
                table: "PHOTO_UserGallery");

            migrationBuilder.RenameTable(
                name: "PHOTO_UserGalleryPhoto",
                newName: "UserGalleryPhoto");

            migrationBuilder.RenameTable(
                name: "PHOTO_UserGallery",
                newName: "UserGallery");

            migrationBuilder.RenameIndex(
                name: "IX_PHOTO_UserGalleryPhoto_UserGalleryId",
                table: "UserGalleryPhoto",
                newName: "IX_UserGalleryPhoto_UserGalleryId");

            migrationBuilder.RenameIndex(
                name: "IX_PHOTO_UserGalleryPhoto_PhotoId",
                table: "UserGalleryPhoto",
                newName: "IX_UserGalleryPhoto_PhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGalleryPhoto",
                table: "UserGalleryPhoto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGallery",
                table: "UserGallery",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGalleryPhoto_PHOTO_Photo_PhotoId",
                table: "UserGalleryPhoto",
                column: "PhotoId",
                principalTable: "PHOTO_Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGalleryPhoto_UserGallery_UserGalleryId",
                table: "UserGalleryPhoto",
                column: "UserGalleryId",
                principalTable: "UserGallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
