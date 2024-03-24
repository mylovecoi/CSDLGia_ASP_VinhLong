using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixGiaRungDM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoaiRung",
                table: "GiaRungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTTR",
                table: "GiaRungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapXep",
                table: "GiaRungDm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaRungDm",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiRung",
                table: "GiaRungDm");

            migrationBuilder.DropColumn(
                name: "MaTTR",
                table: "GiaRungDm");

            migrationBuilder.DropColumn(
                name: "STTSapXep",
                table: "GiaRungDm");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaRungDm");
        }
    }
}
