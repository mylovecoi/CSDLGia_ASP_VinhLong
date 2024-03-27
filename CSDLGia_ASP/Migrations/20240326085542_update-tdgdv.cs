using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatetdgdv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Ngaydungtd",
                table: "ThamDinhGiaDv",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Soqddungtd",
                table: "ThamDinhGiaDv",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ngaydungtd",
                table: "ThamDinhGiaDv");

            migrationBuilder.DropColumn(
                name: "Soqddungtd",
                table: "ThamDinhGiaDv");
        }
    }
}
