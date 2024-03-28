using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiahhst2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ipf1",
                table: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "Ipf2",
                table: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "Ipf3",
                table: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "Ipf4",
                table: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "Ipf5",
                table: "GiaHangHoaTaiSieuThi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ipf1",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf2",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf3",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf4",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipf5",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
