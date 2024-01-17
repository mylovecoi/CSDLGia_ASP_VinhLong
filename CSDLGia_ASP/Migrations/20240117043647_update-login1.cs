using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatelogin1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CaHue",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Etanol",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giay",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HocPhiDaoTaoLaiXe",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SachGk",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SieuThi",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ThucAnChanNuoi",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ThucPhamCn",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VanTaiKhachBangOtoCoDinh",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VanTaiKhachBangXeBuyt",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VlXdCatSan",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VlXdDaXayDung",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VlXdDatSanlap",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "XmThepXd",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaHue",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Etanol",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Giay",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "HocPhiDaoTaoLaiXe",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SachGk",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SieuThi",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ThucAnChanNuoi",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ThucPhamCn",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "VanTaiKhachBangOtoCoDinh",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "VanTaiKhachBangXeBuyt",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "VlXdCatSan",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "VlXdDaXayDung",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "VlXdDatSanlap",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "XmThepXd",
                table: "Company");
        }
    }
}
