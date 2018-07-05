using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Migrations
{
    public partial class ArticleHistoryStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ArticleHistories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "Published" },
                values: new object[] { new Guid("a8cb43d7-0068-480b-806e-197381498dfe"), new DateTime(2018, 7, 5, 15, 41, 28, 572, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "Published" },
                values: new object[] { new Guid("a8cb43d7-0068-480b-806e-197381498dfe"), new DateTime(2018, 7, 5, 15, 41, 28, 572, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ArticleHistories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "Published" },
                values: new object[] { new Guid("e20373cc-26ed-4bb7-8856-ced0ff9c1d5f"), new DateTime(2018, 7, 3, 15, 56, 55, 423, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "Published" },
                values: new object[] { new Guid("e20373cc-26ed-4bb7-8856-ced0ff9c1d5f"), new DateTime(2018, 7, 3, 15, 56, 55, 423, DateTimeKind.Utc) });
        }
    }
}
