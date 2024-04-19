using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class fixKeKhaiDangKyGia1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaCqCq",
                table: "KeKhaiDangKyGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayChuyen",
                table: "KeKhaiDangKyGia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SoDtNguoiChuyen",
                table: "KeKhaiDangKyGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThongTinNguoiChuyen",
                table: "KeKhaiDangKyGia",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaCqCq",
                table: "KeKhaiDangKyGia");

            migrationBuilder.DropColumn(
                name: "NgayChuyen",
                table: "KeKhaiDangKyGia");

            migrationBuilder.DropColumn(
                name: "SoDtNguoiChuyen",
                table: "KeKhaiDangKyGia");

            migrationBuilder.DropColumn(
                name: "ThongTinNguoiChuyen",
                table: "KeKhaiDangKyGia");
        }
    }
}
