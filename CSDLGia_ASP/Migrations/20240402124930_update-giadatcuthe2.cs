using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiadatcuthe2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "GiaDatPhanLoaiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienThi",
                table: "GiaDatPhanLoaiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapXep",
                table: "GiaDatPhanLoaiCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaDatPhanLoaiCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "GiaDatPhanLoaiCt");

            migrationBuilder.DropColumn(
                name: "STTHienThi",
                table: "GiaDatPhanLoaiCt");

            migrationBuilder.DropColumn(
                name: "STTSapXep",
                table: "GiaDatPhanLoaiCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaDatPhanLoaiCt");
        }
    }
}
