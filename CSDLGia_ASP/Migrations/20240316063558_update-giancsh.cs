using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiancsh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SttHienthi",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SttHienthi",
                table: "GiaNuocShDmVung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaNuocShDmVung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "GiaNuocShDmKhung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "GiaNuocShDmKhung",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaNuocShDmKhung",
                type: "nvarchar(max)",
                nullable: true);

            /*migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaDatDiaBanCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaDatDiaBanCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaLoaiDat",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SttHienthi",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "SttHienthi",
                table: "GiaNuocShDmVung");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaNuocShDmVung");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "GiaNuocShDmKhung");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "GiaNuocShDmKhung");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaNuocShDmKhung");

            /*migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "GiaDatDiaBan");

            migrationBuilder.DropColumn(
                name: "MaLoaiDat",
                table: "GiaDatDiaBan");*/
        }
    }
}
