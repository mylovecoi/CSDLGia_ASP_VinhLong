using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiahhst3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nam",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thang",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nam",
                table: "GiaHangHoaTaiSieuThi");

            migrationBuilder.DropColumn(
                name: "Thang",
                table: "GiaHangHoaTaiSieuThi");
        }
    }
}
