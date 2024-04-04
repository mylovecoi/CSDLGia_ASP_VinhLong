using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedistricttown4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matinh",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Matinh",
                table: "Districts");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "Towns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "Towns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "Districts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "Districts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "Districts");

            migrationBuilder.AddColumn<string>(
                name: "Matinh",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Matinh",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
