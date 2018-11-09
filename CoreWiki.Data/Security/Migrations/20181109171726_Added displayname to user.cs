using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Data.EntityFramework.Security.Migrations
{
    public partial class Addeddisplaynametouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9d0a35d-5796-41c1-b0d9-b3ebddfab84c", "5c4da24f-6b22-4549-a3a0-22a0c5c195d5", "Authors", "AUTHORS" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "682a9c69-f81f-4df4-aaea-b882a56f0087", "91ca8adb-68ce-4eca-b427-8058d52ee8e9", "Editors", "EDITORS" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44cec988-7324-4a8d-908b-fb3f965777e8", "f557ba2b-4bc6-400c-8087-1ee80eb8928f", "Administrators", "ADMINISTRATORS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "44cec988-7324-4a8d-908b-fb3f965777e8", "f557ba2b-4bc6-400c-8087-1ee80eb8928f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "682a9c69-f81f-4df4-aaea-b882a56f0087", "91ca8adb-68ce-4eca-b427-8058d52ee8e9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e9d0a35d-5796-41c1-b0d9-b3ebddfab84c", "5c4da24f-6b22-4549-a3a0-22a0c5c195d5" });

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

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
    }
}
