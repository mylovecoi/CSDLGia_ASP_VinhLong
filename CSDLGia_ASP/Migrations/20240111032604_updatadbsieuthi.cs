using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatadbsieuthi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Madoituong",
                table: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi",
                newName: "Tenhanghoa");

            migrationBuilder.AddColumn<string>(
                name: "Mahanghoa",
                table: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mahanghoa",
                table: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi");

            migrationBuilder.RenameColumn(
                name: "Tenhanghoa",
                table: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi",
                newName: "Madoituong");
        }
    }
}
