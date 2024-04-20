using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatekkdkg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thydggadgia",
                table: "KeKhaiDangKyGia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ytcauthanhgia",
                table: "KeKhaiDangKyGia",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thydggadgia",
                table: "KeKhaiDangKyGia");

            migrationBuilder.DropColumn(
                name: "Ytcauthanhgia",
                table: "KeKhaiDangKyGia");
        }
    }
}
