using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class update_HeThong_20240409 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileDangKy",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileDangKyBase64",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileHDSD",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileHDSDBase64",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileQuyChe",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileQuyCheBase64",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkAPIXacthuc",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaDiaBanHanhChinh",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaDonViThuThap",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TokenLGSP",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileDangKy",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "FileDangKyBase64",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "FileHDSD",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "FileHDSDBase64",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "FileQuyChe",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "FileQuyCheBase64",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "LinkAPIXacthuc",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "MaDiaBanHanhChinh",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "MaDonViThuThap",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "TokenLGSP",
                table: "tblHeThong");
        }
    }
}
