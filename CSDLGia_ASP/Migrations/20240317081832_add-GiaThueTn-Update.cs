using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addGiaThueTnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SapXep",
                table: "GiaThueTaiNguyenCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Style",
                table: "GiaThueTaiNguyenCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SapXep",
                table: "GiaThueTaiNguyenCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaThueTaiNguyenCt");

            migrationBuilder.DropColumn(
                name: "LoaiDat",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "MaNhom",
                table: "GiaThueMatDatMatNuocCt");
        }
    }
}
