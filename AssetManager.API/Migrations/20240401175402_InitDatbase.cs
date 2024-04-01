using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.API.Migrations
{
    /// <inheritdoc />
    public partial class InitDatbase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Guid = table.Column<string>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourcePackages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Max = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Min = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Patch = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    AuditStatus = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    TargetProjectId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcePackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourcePackages_ProjectItems_TargetProjectId",
                        column: x => x.TargetProjectId,
                        principalTable: "ProjectItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformAssets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TargetPlatformId = table.Column<long>(type: "INTEGER", nullable: false),
                    AssetPath = table.Column<string>(type: "TEXT", nullable: true),
                    TargetAssetPackageId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformAssets_Platforms_TargetPlatformId",
                        column: x => x.TargetPlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformAssets_ResourcePackages_TargetAssetPackageId",
                        column: x => x.TargetAssetPackageId,
                        principalTable: "ResourcePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAssets_TargetAssetPackageId",
                table: "PlatformAssets",
                column: "TargetAssetPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAssets_TargetPlatformId",
                table: "PlatformAssets",
                column: "TargetPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_Name",
                table: "Platforms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourcePackages_TargetProjectId",
                table: "ResourcePackages",
                column: "TargetProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformAssets");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "ResourcePackages");

            migrationBuilder.DropTable(
                name: "ProjectItems");
        }
    }
}
