using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedbgiaxaydungmoict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tennhom",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Thoidiem",
                table: "GiaXayDungMoiCt",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tennhom",
                table: "GiaXayDungMoiCt");

            migrationBuilder.DropColumn(
                name: "Thoidiem",
                table: "GiaXayDungMoiCt");
        }
    }
}
