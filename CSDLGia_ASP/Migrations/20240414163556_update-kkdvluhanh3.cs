using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatekkdvluhanh3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "KkGiaLuHanhCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "KkGiaLuHanhCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "KkGiaLuHanhCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "KkGiaLuHanhCt");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "KkGiaLuHanhCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "KkGiaLuHanhCt");
        }
    }
}
