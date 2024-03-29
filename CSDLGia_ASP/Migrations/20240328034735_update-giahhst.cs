using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiahhst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doituongsd",
                table: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "Doituongsd",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Giachuathue",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Giachuathue1",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Giachuathue2",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Giacothue",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Madoituong",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Namchuathue",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Namchuathue1",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Namchuathue2",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Namchuathue3",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.RenameColumn(
                name: "Thuevat",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Tenhanghoa");

            migrationBuilder.RenameColumn(
                name: "Thanhtien",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "Phibvmttyle",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "STTHienthi");

            migrationBuilder.RenameColumn(
                name: "Phibvmt",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Mahanghoa");

            migrationBuilder.RenameColumn(
                name: "Namchuathue4",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Madv");

            migrationBuilder.RenameColumn(
                name: "Giachuathue4",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Giatu");

            migrationBuilder.RenameColumn(
                name: "Giachuathue3",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Giaden");

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.RenameColumn(
                name: "Tenhanghoa",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Thuevat");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Thanhtien");

            migrationBuilder.RenameColumn(
                name: "STTHienthi",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Phibvmttyle");

            migrationBuilder.RenameColumn(
                name: "Mahanghoa",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Phibvmt");

            migrationBuilder.RenameColumn(
                name: "Madv",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Namchuathue4");

            migrationBuilder.RenameColumn(
                name: "Giatu",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Giachuathue4");

            migrationBuilder.RenameColumn(
                name: "Giaden",
                table: "GiaHangHoaTaiSieuThiCt",
                newName: "Giachuathue3");

            migrationBuilder.AddColumn<string>(
                name: "Doituongsd",
                table: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Doituongsd",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Giachuathue",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giachuathue1",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giachuathue2",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Giacothue",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Madoituong",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Namchuathue",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Namchuathue1",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Namchuathue2",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Namchuathue3",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
