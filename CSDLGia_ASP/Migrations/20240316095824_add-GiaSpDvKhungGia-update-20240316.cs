using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addGiaSpDvKhungGiaupdate20240316 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Giatoida",
                table: "GiaSpDvKhungGiaDm");

            migrationBuilder.DropColumn(
                name: "Giatoithieu",
                table: "GiaSpDvKhungGiaDm");

            migrationBuilder.RenameColumn(
                name: "Hientrang",
                table: "GiaSpDvKhungGiaDm",
                newName: "HienTrang");

            migrationBuilder.RenameColumn(
                name: "Stt",
                table: "GiaSpDvKhungGiaDm",
                newName: "SapXep");

            migrationBuilder.RenameColumn(
                name: "Phanloai",
                table: "GiaSpDvKhungGiaDm",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "GiaSpDvKhungGiaDm",
                newName: "Maso");

            migrationBuilder.AddColumn<string>(
                name: "Capdo",
                table: "GiaSpDvKhungGiaDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaSpDvKhungGiaDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Magoc",
                table: "GiaSpDvKhungGiaDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaSpDvKhungGiaCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SapXep",
                table: "GiaSpDvKhungGiaCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaSpDvKhungGiaCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capdo",
                table: "GiaSpDvKhungGiaDm");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaSpDvKhungGiaDm");

            migrationBuilder.DropColumn(
                name: "Magoc",
                table: "GiaSpDvKhungGiaDm");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaSpDvKhungGiaCt");

            migrationBuilder.DropColumn(
                name: "SapXep",
                table: "GiaSpDvKhungGiaCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaSpDvKhungGiaCt");

            migrationBuilder.RenameColumn(
                name: "HienTrang",
                table: "GiaSpDvKhungGiaDm",
                newName: "Hientrang");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "GiaSpDvKhungGiaDm",
                newName: "Phanloai");

            migrationBuilder.RenameColumn(
                name: "SapXep",
                table: "GiaSpDvKhungGiaDm",
                newName: "Stt");

            migrationBuilder.RenameColumn(
                name: "Maso",
                table: "GiaSpDvKhungGiaDm",
                newName: "Level");

            migrationBuilder.AddColumn<double>(
                name: "Giatoida",
                table: "GiaSpDvKhungGiaDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giatoithieu",
                table: "GiaSpDvKhungGiaDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
