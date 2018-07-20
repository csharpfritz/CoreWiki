using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Migrations
{
    public partial class ArticleHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticleHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArticleId = table.Column<int>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Topic = table.Column<string>(maxLength: 100, nullable: false),
                    Slug = table.Column<string>(nullable: true),
                    Published = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleHistories_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleHistories_ArticleId",
                table: "ArticleHistories",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleHistories");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Articles");
        }
    }
}
