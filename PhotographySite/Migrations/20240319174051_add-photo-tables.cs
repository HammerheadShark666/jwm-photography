using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographySite.Migrations
{
    /// <inheritdoc />
    public partial class addphototables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Gallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO_Gallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Montage",
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
                    table.PrimaryKey("PK_PHOTO_Montage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Orientation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO_Orientation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Palette",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO_Palette", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Photo",
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
                    table.PrimaryKey("PK_PHOTO_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PHOTO_Photo_PHOTO_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PHOTO_Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PHOTO_Photo_PHOTO_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "PHOTO_Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PHOTO_Photo_PHOTO_Palette_PaletteId",
                        column: x => x.PaletteId,
                        principalTable: "PHOTO_Palette",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_Favourite",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO_Favourite", x => new { x.UserId, x.PhotoId });
                    table.ForeignKey(
                        name: "FK_PHOTO_Favourite_PHOTO_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "PHOTO_Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PHOTO_GalleryPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO_GalleryPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PHOTO_GalleryPhoto_PHOTO_Gallery_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "PHOTO_Gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PHOTO_GalleryPhoto_PHOTO_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "PHOTO_Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_UserGalleryPhoto_PHOTO_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "PHOTO_Photo",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PHOTO_Favourite_PhotoId",
                table: "PHOTO_Favourite",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PHOTO_GalleryPhoto_GalleryId",
                table: "PHOTO_GalleryPhoto",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_PHOTO_GalleryPhoto_PhotoId",
                table: "PHOTO_GalleryPhoto",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PHOTO_Photo_CategoryId",
                table: "PHOTO_Photo",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PHOTO_Photo_CountryId",
                table: "PHOTO_Photo",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PHOTO_Photo_PaletteId",
                table: "PHOTO_Photo",
                column: "PaletteId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PHOTO_Favourite");

            migrationBuilder.DropTable(
                name: "PHOTO_GalleryPhoto");

            migrationBuilder.DropTable(
                name: "PHOTO_Montage");

            migrationBuilder.DropTable(
                name: "PHOTO_Orientation");

            migrationBuilder.DropTable(
                name: "UserGalleryPhoto");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PHOTO_Gallery");

            migrationBuilder.DropTable(
                name: "PHOTO_Photo");

            migrationBuilder.DropTable(
                name: "UserGallery");

            migrationBuilder.DropTable(
                name: "PHOTO_Category");

            migrationBuilder.DropTable(
                name: "PHOTO_Country");

            migrationBuilder.DropTable(
                name: "PHOTO_Palette");
        }
    }
}
