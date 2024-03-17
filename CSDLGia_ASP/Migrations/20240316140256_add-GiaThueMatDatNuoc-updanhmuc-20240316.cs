using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addGiaThueMatDatNuocupdanhmuc20240316 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dongia",
                table: "GiaThueMatDatMatNuocCt",
                newName: "SapXep");

            migrationBuilder.AddColumn<string>(
               name: "LoaiDat",
               table: "GiaThueMatDatMatNuocCt",
               type: "nvarchar(max)",
               nullable: true);

            migrationBuilder.AddColumn<string>(
              name: "MaNhom",
              table: "GiaThueMatDatMatNuocCt",
              type: "nvarchar(max)",
              nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Capdo",
                table: "GiaThueMatDatMatNuocDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaThueMatDatMatNuocDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Magoc",
                table: "GiaThueMatDatMatNuocDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaThueMatDatMatNuocDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maso",
                table: "GiaThueMatDatMatNuocDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SapXep",
                table: "GiaThueMatDatMatNuocDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaThueMatDatMatNuocDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Dongia1",
                table: "GiaThueMatDatMatNuocCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Dongia2",
                table: "GiaThueMatDatMatNuocCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Dongia3",
                table: "GiaThueMatDatMatNuocCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Dongia4",
                table: "GiaThueMatDatMatNuocCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Dongia5",
                table: "GiaThueMatDatMatNuocCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaThueMatDatMatNuocCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaThueMatDatMatNuocCt",
                type: "nvarchar(max)",
                nullable: true);            

            migrationBuilder.CreateTable(
                name: "GiaThueMatDatMatNuocNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueMatDatMatNuocNhom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaThueMatDatMatNuocNhom");

            migrationBuilder.DropColumn(
                name: "Capdo",
                table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
                name: "Magoc",
                table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
                name: "Maso",
                table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
                name: "SapXep",
                table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
               name: "LoaiDat",
               table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
              name: "MaNhom",
              table: "GiaThueMatDatMatNuocDm");

            migrationBuilder.DropColumn(
                name: "Dongia1",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "Dongia2",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "Dongia3",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "Dongia4",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "Dongia5",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaThueMatDatMatNuocCt");            

            migrationBuilder.RenameColumn(
                name: "SapXep",
                table: "GiaThueMatDatMatNuocCt",
                newName: "Dongia");
        }
    }
}
