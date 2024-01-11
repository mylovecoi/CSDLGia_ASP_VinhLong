using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedbgiasieuthi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Madoituong",
                table: "GiaHangHoaTaiSieuThiDmSieuThi",
                newName: "Tensieuthi");

            migrationBuilder.RenameColumn(
                name: "Doituongsd",
                table: "GiaHangHoaTaiSieuThiDmSieuThi",
                newName: "Masieuthi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tensieuthi",
                table: "GiaHangHoaTaiSieuThiDmSieuThi",
                newName: "Madoituong");

            migrationBuilder.RenameColumn(
                name: "Masieuthi",
                table: "GiaHangHoaTaiSieuThiDmSieuThi",
                newName: "Doituongsd");
        }
    }
}
