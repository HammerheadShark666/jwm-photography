using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotographySite.Migrations
{
    /// <inheritdoc />
    public partial class createdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Montage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Column = table.Column<byte>(type: "tinyint", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false),
                    Orientation = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Montage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orientation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orientation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palette",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palette", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Showcase",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showcase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShowcasePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowcaseId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowcasePhoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryPhoto_Gallery_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Camera = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Lens = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ExposureTime = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    AperturValue = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    ExposureProgram = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Iso = table.Column<int>(type: "int", nullable: false),
                    DateTaken = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    FocalLength = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Orientation = table.Column<byte>(type: "tinyint", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    UseInMontage = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    PaletteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photo_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photo_Palette_PaletteId",
                        column: x => x.PaletteId,
                        principalTable: "Palette",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Landscape" },
                    { 2, "Travel" },
                    { 3, "Wildlife" },
                    { 4, "Underwater" },
                    { 5, "Portrait" },
                    { 6, "Macro" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "England" },
                    { 2, "Scotland" },
                    { 3, "Wales" },
                    { 5, "India" },
                    { 6, "Australia" },
                    { 7, "Nepal" },
                    { 8, "Tibet" },
                    { 9, "China" },
                    { 10, "Vietnam" },
                    { 11, "Cambodia" },
                    { 12, "Thailand" },
                    { 13, "Malaysia" },
                    { 14, "Borneo" },
                    { 15, "Philippines" },
                    { 16, "Egypt" },
                    { 17, "Indonesia" },
                    { 18, "Peru" },
                    { 19, "Bolivia" },
                    { 20, "Chile" },
                    { 21, "Argentina" },
                    { 22, "Germany" },
                    { 23, "Spain" }
                });

            migrationBuilder.InsertData(
                table: "Gallery",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Landscape" },
                    { 2, "Travel" },
                    { 3, "Wildlife" },
                    { 4, "Underwater" },
                    { 5, "Portraits" },
                    { 6, "Black & White" },
                    { 7, "Macro" }
                });

            migrationBuilder.InsertData(
                table: "Montage",
                columns: new[] { "Id", "Column", "Order", "Orientation" },
                values: new object[,]
                {
                    { 1, (byte)1, (byte)1, (byte)1 },
                    { 2, (byte)1, (byte)2, (byte)0 },
                    { 3, (byte)1, (byte)3, (byte)2 },
                    { 4, (byte)1, (byte)4, (byte)2 },
                    { 5, (byte)2, (byte)1, (byte)0 },
                    { 6, (byte)2, (byte)2, (byte)1 },
                    { 7, (byte)2, (byte)3, (byte)2 },
                    { 8, (byte)2, (byte)4, (byte)2 },
                    { 9, (byte)2, (byte)5, (byte)0 },
                    { 10, (byte)2, (byte)6, (byte)0 },
                    { 11, (byte)3, (byte)1, (byte)2 },
                    { 12, (byte)3, (byte)2, (byte)0 },
                    { 13, (byte)4, (byte)1, (byte)1 },
                    { 14, (byte)4, (byte)2, (byte)1 },
                    { 15, (byte)4, (byte)3, (byte)2 },
                    { 16, (byte)4, (byte)4, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Orientation",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Landscape" },
                    { 2, "Portrait" },
                    { 3, "Square" }
                });

            migrationBuilder.InsertData(
                table: "Palette",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Colour" },
                    { 2, "Black & White" },
                    { 3, "Infrared" }
                });

            migrationBuilder.InsertData(
                table: "Showcase",
                columns: new[] { "Id", "DateFrom", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black & White" },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mountains" },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atacama Desert" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryPhoto_GalleryId",
                table: "GalleryPhoto",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_CategoryId",
                table: "Photo",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_CountryId",
                table: "Photo",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_PaletteId",
                table: "Photo",
                column: "PaletteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryPhoto");

            migrationBuilder.DropTable(
                name: "Montage");

            migrationBuilder.DropTable(
                name: "Orientation");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Showcase");

            migrationBuilder.DropTable(
                name: "ShowcasePhoto");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Palette");
        }
    }
}
