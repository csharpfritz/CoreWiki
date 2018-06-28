using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Areas.Identity.Data.Migrations
{
    public partial class CanNotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanNotify",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ca00d44-70cc-44a1-947d-a8daeb526ef0", "615c7a08-8148-4add-b333-f5ca305400ac", "Authors", "Authors" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8f0e30f-73f2-44db-90b3-11797e5d2c5c", "4f044959-1a5e-488b-a12d-daa39b5b2722", "Editors", "Editors" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b46d803-42ab-411a-ad96-21f58234c3fc", "d2e1787b-db7a-424e-8ecb-56754fc0d71a", "Administrators", "Administrators" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5ca00d44-70cc-44a1-947d-a8daeb526ef0", "615c7a08-8148-4add-b333-f5ca305400ac" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8b46d803-42ab-411a-ad96-21f58234c3fc", "d2e1787b-db7a-424e-8ecb-56754fc0d71a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a8f0e30f-73f2-44db-90b3-11797e5d2c5c", "4f044959-1a5e-488b-a12d-daa39b5b2722" });

            migrationBuilder.DropColumn(
                name: "CanNotify",
                table: "AspNetUsers");
        }
    }
}
