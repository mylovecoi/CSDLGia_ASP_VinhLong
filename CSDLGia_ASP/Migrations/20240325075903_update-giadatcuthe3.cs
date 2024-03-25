using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiadatcuthe3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExcel",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "Ipf1",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "Ipf2",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "Ipf3",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "Ipf4",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "Ipf5",
                table: "GiaDatPhanLoai");

            migrationBuilder.DropColumn(
                name: "PhanLoaiHoSo",
                table: "GiaDatPhanLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeExcel",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf1",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf2",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf3",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf4",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf5",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanLoaiHoSo",
                table: "GiaDatPhanLoai",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
