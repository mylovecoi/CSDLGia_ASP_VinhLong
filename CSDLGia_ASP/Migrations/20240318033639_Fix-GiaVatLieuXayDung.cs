using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixGiaVatLieuXayDung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "GiaVatLieuXayDungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienThi",
                table: "GiaVatLieuXayDungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapXep",
                table: "GiaVatLieuXayDungCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaVatLieuXayDungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TieuChuan",
                table: "GiaVatLieuXayDungCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "GiaVatLieuXayDungCt");

            migrationBuilder.DropColumn(
                name: "STTHienThi",
                table: "GiaVatLieuXayDungCt");

            migrationBuilder.DropColumn(
                name: "STTSapXep",
                table: "GiaVatLieuXayDungCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaVatLieuXayDungCt");

            migrationBuilder.DropColumn(
                name: "TieuChuan",
                table: "GiaVatLieuXayDungCt");
        }
    }
}
