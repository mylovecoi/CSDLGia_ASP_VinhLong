using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class suagiadatphanloaiexcel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lydo_ad",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Lydo_h",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Lydo_t",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Macqcq_ad",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Macqcq_h",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Macqcq_t",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Madv_ad",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Madv_h",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Madv_t",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Thoidiem_ad",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Thoidiem_h",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Thoidiem_t",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Thongtin_ad",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Thongtin_h",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Thongtin_t",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Trangthai_ad",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Trangthai_h",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.DropColumn(
                name: "Trangthai_t",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.AddColumn<int>(
                name: "LineStart",
                table: "GiaDatPhanLoaiExcel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineStart",
                table: "GiaDatPhanLoaiExcel");

            migrationBuilder.AddColumn<string>(
                name: "Lydo_ad",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lydo_h",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lydo_t",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Macqcq_ad",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Macqcq_h",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Macqcq_t",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Madv_ad",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Madv_h",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Madv_t",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Thoidiem_ad",
                table: "GiaDatPhanLoaiExcel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Thoidiem_h",
                table: "GiaDatPhanLoaiExcel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Thoidiem_t",
                table: "GiaDatPhanLoaiExcel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Thongtin_ad",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thongtin_h",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thongtin_t",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trangthai_ad",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trangthai_h",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trangthai_t",
                table: "GiaDatPhanLoaiExcel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
