using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategialephitbgiancsh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Stt",
                table: "GiaPhiLePhiDm",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CapDo",
                table: "GiaPhiLePhiDm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSo",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSoGoc",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Giaden",
                table: "GiaPhiLePhiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giatu",
                table: "GiaPhiLePhiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CapDo",
                table: "GiaNuocShDmVung",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaNuocShDmVung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSo",
                table: "GiaNuocShDmVung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSoGoc",
                table: "GiaNuocShDmVung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STT",
                table: "GiaNuocShDmVung",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DonGia1",
                table: "GiaNuocShCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DonGia2",
                table: "GiaNuocShCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SanLuong",
                table: "GiaNuocShCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TyTrongTieuThu",
                table: "GiaNuocShCt",
                type: "nvarchar(max)",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaDatDiaBanCt",
                type: "nvarchar(max)",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaDatDiaBanCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapDo",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "MaSo",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "MaSoGoc",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "Giaden",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "Giatu",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "CapDo",
                table: "GiaNuocShDmVung");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaNuocShDmVung");

            migrationBuilder.DropColumn(
                name: "MaSo",
                table: "GiaNuocShDmVung");

            migrationBuilder.DropColumn(
                name: "MaSoGoc",
                table: "GiaNuocShDmVung");

            migrationBuilder.DropColumn(
                name: "STT",
                table: "GiaNuocShDmVung");

            migrationBuilder.DropColumn(
                name: "DonGia1",
                table: "GiaNuocShCt");

            migrationBuilder.DropColumn(
                name: "DonGia2",
                table: "GiaNuocShCt");

            migrationBuilder.DropColumn(
                name: "SanLuong",
                table: "GiaNuocShCt");

            migrationBuilder.DropColumn(
                name: "TyTrongTieuThu",
                table: "GiaNuocShCt");

            migrationBuilder.AlterColumn<string>(
                name: "Stt",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaDatDiaBanCt");
            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaDatDiaBanCt");
        }
    }
}
