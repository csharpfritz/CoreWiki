using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Migrations
{
    public partial class AddAuthorNametoarticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ArticleHistories_Articles_ArticleId",
            //    table: "ArticleHistories");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Articles",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ArticleId",
            //    table: "ArticleHistories",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "ArticleHistories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "ArticleHistories",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ArticleHistories",
                columns: new[] { "Id", "ArticleId", "AuthorId", "AuthorName", "Content", "Published", "Slug", "Topic", "Version" },
                values: new object[] { 1, 1, new Guid("00000000-0000-0000-0000-000000000000"), "Unknown", "This is the default home page.  Please change me!", new DateTime(2018, 6, 19, 14, 31, 2, 265, DateTimeKind.Utc), "home-page", "HomePage", 1 });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "AuthorName", "Published", "Version" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "Unknown", new DateTime(2018, 6, 19, 14, 31, 2, 265, DateTimeKind.Utc), 1 });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ArticleHistories_Articles_ArticleId",
            //    table: "ArticleHistories",
            //    column: "ArticleId",
            //    principalTable: "Articles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleHistories_Articles_ArticleId",
                table: "ArticleHistories");

            migrationBuilder.DeleteData(
                table: "ArticleHistories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "ArticleHistories");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "ArticleHistories");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "ArticleHistories",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "Published", "Version" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2018, 6, 19, 14, 31, 2, 265, DateTimeKind.Utc), 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleHistories_Articles_ArticleId",
                table: "ArticleHistories",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
