using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Migrations
{
    public partial class AddSlugHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlugHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArticleId = table.Column<int>(nullable: true),
                    OldSlug = table.Column<string>(nullable: true),
                    Added = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlugHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlugHistories_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlugHistories_ArticleId",
                table: "SlugHistories",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_SlugHistories_OldSlug_Added",
                table: "SlugHistories",
                columns: new[] { "OldSlug", "Added" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlugHistories");
        }
    }
}
