using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreWiki.Migrations
{
    public partial class AddedReadCountfieldtoArticlesdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ReadCount",
                table: "Articles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadCount",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
