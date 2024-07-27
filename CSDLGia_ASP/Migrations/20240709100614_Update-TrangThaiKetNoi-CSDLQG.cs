using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrangThaiKetNoiCSDLQG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "ThamDinhGia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "ThamDinhGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaThueTaiNguyenNhom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaThueTaiNguyenNhom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaThueTaiNguyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaThueTaiNguyen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaSpDvToiDaNhom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaSpDvToiDaNhom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaSpDvToiDa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaSpDvToiDa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaHhDvkTh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvkTh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetNoi",
                table: "GiaHhDvkNhom",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvkNhom",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "ThamDinhGia");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "ThamDinhGia");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaThueTaiNguyenNhom");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaThueTaiNguyenNhom");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaThueTaiNguyen");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaThueTaiNguyen");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaSpDvToiDaNhom");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaSpDvToiDaNhom");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaSpDvToiDa");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaSpDvToiDa");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvkTh");

            migrationBuilder.DropColumn(
                name: "NgayKetNoi",
                table: "GiaHhDvkNhom");

            migrationBuilder.DropColumn(
                name: "TrangThaiCSDLQG",
                table: "GiaHhDvkNhom");
        }
    }
}
