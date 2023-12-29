using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class Update_API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "KetNoiAPI_HoSo_ChiTiet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "KetNoiAPI_HoSo_ChiTiet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "KetNoiAPI_HoSo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "KetNoiAPI_HoSo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "KetNoiAPI",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "KetNoiAPI",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "KetNoiAPI_HoSo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "KetNoiAPI_HoSo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "KetNoiAPI_HoSo");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "KetNoiAPI_HoSo");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "KetNoiAPI");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "KetNoiAPI");
        }
    }
}
