using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Data.Security.Migrations
{
    public partial class NormalizedRoleNamesToUpper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b36ebe22-2608-4117-96ac-7b9b6348fa7a", "d8ec32b2-157d-4b1d-a1a1-0a4c26ef4bfa", "Authors", "AUTHORS" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bba645af-8898-4def-a9c7-b9b428b5538a", "48e15fd0-adb9-4ac3-855b-fb40b9804ae7", "Editors", "EDITORS" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71f45640-c52a-4ea6-9a9f-40850ba153a8", "6baba63f-b98c-4fdc-8338-e081719bb139", "Administrators", "ADMINISTRATORS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "71f45640-c52a-4ea6-9a9f-40850ba153a8", "6baba63f-b98c-4fdc-8338-e081719bb139" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b36ebe22-2608-4117-96ac-7b9b6348fa7a", "d8ec32b2-157d-4b1d-a1a1-0a4c26ef4bfa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "bba645af-8898-4def-a9c7-b9b428b5538a", "48e15fd0-adb9-4ac3-855b-fb40b9804ae7" });

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
    }
}
