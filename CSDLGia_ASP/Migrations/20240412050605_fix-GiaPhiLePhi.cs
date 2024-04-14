using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class fixGiaPhiLePhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NhanHieu",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NuocSxLr",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoNguoiTaiTrong",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TheTich",
                table: "GiaPhiLePhiDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DonGia",
                table: "GiaPhiLePhiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NhanHieu",
                table: "GiaPhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NuocSxLr",
                table: "GiaPhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoNguoiTaiTrong",
                table: "GiaPhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TheTich",
                table: "GiaPhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NhanHieu",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "NuocSxLr",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "SoNguoiTaiTrong",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "TheTich",
                table: "GiaPhiLePhiDm");

            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "NhanHieu",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "NuocSxLr",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "SoNguoiTaiTrong",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "TheTich",
                table: "GiaPhiLePhiCt");
        }
    }
}
