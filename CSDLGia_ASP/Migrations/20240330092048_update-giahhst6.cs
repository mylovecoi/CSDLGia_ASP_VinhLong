using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiahhst6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Matt",
                table: "GiaHangHoaTaiSieuThiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Matt",
                table: "GiaHangHoaTaiSieuThi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matt",
                table: "GiaHangHoaTaiSieuThiCt");

            migrationBuilder.DropColumn(
                name: "Matt",
                table: "GiaHangHoaTaiSieuThi");
        }
    }
}
