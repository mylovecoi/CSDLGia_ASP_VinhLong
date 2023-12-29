using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiaodichbds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cap1",
                table: "GiaThueNhaSVCt");

            migrationBuilder.DropColumn(
                name: "Cap2",
                table: "GiaThueNhaSVCt");

            migrationBuilder.DropColumn(
                name: "Cap3",
                table: "GiaThueNhaSVCt");

            migrationBuilder.DropColumn(
                name: "Cap4",
                table: "GiaThueNhaSVCt");

            migrationBuilder.DropColumn(
                name: "Cap5",
                table: "GiaThueNhaSVCt");

            migrationBuilder.DropColumn(
                name: "Dvt",
                table: "GiaThueNhaSVCt");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "GiaThueNhaSVCt");

            migrationBuilder.DropColumn(
                name: "Diemcuoi",
                table: "GiaGiaoDichBDSCt");

            migrationBuilder.DropColumn(
                name: "Dientich",
                table: "GiaGiaoDichBDSCt");

            migrationBuilder.DropColumn(
                name: "Vitri",
                table: "GiaGiaoDichBDSCt");

            migrationBuilder.RenameColumn(
                name: "Maloaidat",
                table: "GiaGiaoDichBDSDm",
                newName: "Theodoi");

            migrationBuilder.RenameColumn(
                name: "Loaidat",
                table: "GiaGiaoDichBDSDm",
                newName: "Ten");

            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "GiaGiaoDichBDSCt",
                newName: "Trangthai");

            migrationBuilder.RenameColumn(
                name: "Dongia",
                table: "GiaGiaoDichBDSCt",
                newName: "Gia");

            migrationBuilder.RenameColumn(
                name: "Diemdau",
                table: "GiaGiaoDichBDSCt",
                newName: "Ten");

            migrationBuilder.AddColumn<string>(
                name: "Cap1",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap2",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap3",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap4",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap5",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dvt",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sapxep",
                table: "GiaGiaoDichBDSDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cqbh",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf1",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf2",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf3",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf4",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf5",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Soqdlk",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Thoidiemlk",
                table: "GiaGiaoDichBDS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Tinhtrang",
                table: "GiaGiaoDichBDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiaGiaoDichBDSNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaGiaoDichBDSNhom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaGiaoDichBDSNhom");

            migrationBuilder.DropColumn(
                name: "Cap1",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Cap2",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Cap3",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Cap4",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Cap5",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Dvt",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaGiaoDichBDSDm");

            migrationBuilder.DropColumn(
                name: "Cqbh",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Ipf1",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Ipf2",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Ipf3",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Ipf4",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Ipf5",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Soqdlk",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Thoidiemlk",
                table: "GiaGiaoDichBDS");

            migrationBuilder.DropColumn(
                name: "Tinhtrang",
                table: "GiaGiaoDichBDS");

            migrationBuilder.RenameColumn(
                name: "Theodoi",
                table: "GiaGiaoDichBDSDm",
                newName: "Maloaidat");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "GiaGiaoDichBDSDm",
                newName: "Loaidat");

            migrationBuilder.RenameColumn(
                name: "Trangthai",
                table: "GiaGiaoDichBDSCt",
                newName: "Mota");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "GiaGiaoDichBDSCt",
                newName: "Diemdau");

            migrationBuilder.RenameColumn(
                name: "Gia",
                table: "GiaGiaoDichBDSCt",
                newName: "Dongia");

            migrationBuilder.AddColumn<string>(
                name: "Cap1",
                table: "GiaThueNhaSVCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap2",
                table: "GiaThueNhaSVCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap3",
                table: "GiaThueNhaSVCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap4",
                table: "GiaThueNhaSVCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap5",
                table: "GiaThueNhaSVCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dvt",
                table: "GiaThueNhaSVCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "GiaThueNhaSVCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diemcuoi",
                table: "GiaGiaoDichBDSCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Dientich",
                table: "GiaGiaoDichBDSCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Vitri",
                table: "GiaGiaoDichBDSCt",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
