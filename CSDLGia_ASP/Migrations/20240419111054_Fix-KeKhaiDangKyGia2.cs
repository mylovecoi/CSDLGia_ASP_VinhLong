using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixKeKhaiDangKyGia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HinhThucKinhDoanh",
                table: "KeKhaiDangKyGiaCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuyCachChatLuong",
                table: "KeKhaiDangKyGiaCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenDvCungUng",
                table: "KeKhaiDangKyGiaCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThoiGianThucHien",
                table: "KeKhaiDangKyGiaCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhThucKinhDoanh",
                table: "KeKhaiDangKyGiaCt");

            migrationBuilder.DropColumn(
                name: "QuyCachChatLuong",
                table: "KeKhaiDangKyGiaCt");

            migrationBuilder.DropColumn(
                name: "TenDvCungUng",
                table: "KeKhaiDangKyGiaCt");

            migrationBuilder.DropColumn(
                name: "ThoiGianThucHien",
                table: "KeKhaiDangKyGiaCt");
        }
    }
}
