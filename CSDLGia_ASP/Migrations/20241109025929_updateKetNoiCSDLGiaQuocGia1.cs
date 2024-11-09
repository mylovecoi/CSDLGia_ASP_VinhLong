using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class updateKetNoiCSDLGiaQuocGia1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "ThamDinhGiaHD",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "ThamDinhGiaHD",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "PhiLePhiNhom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "PhiLePhiNhom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "PhiLePhi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "PhiLePhi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "KeKhaiDangKyGia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "KeKhaiDangKyGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaTaiSanCong",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaTaiSanCong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaHhDvCnNhom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvCnNhom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaHhDvCn",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvCn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaGiaoDichBDSNhom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaGiaoDichBDSNhom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaGiaoDichBDS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaDvGdDtNhom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaDvGdDtNhom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaDvGdDt",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaDvGdDt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "Company",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "ThamDinhGiaHD");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "ThamDinhGiaHD");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "PhiLePhiNhom");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "PhiLePhiNhom");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "PhiLePhi");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "PhiLePhi");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "KeKhaiDangKyGia");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "KeKhaiDangKyGia");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaTaiSanCong");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaTaiSanCong");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaHhDvCnNhom");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvCnNhom");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaHhDvCn");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvCn");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaGiaoDichBDSNhom");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaGiaoDichBDSNhom");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaDvGdDtNhom");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaDvGdDtNhom");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaDvGdDt");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaDvGdDt");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "Company");
        }
    }
}
