using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1115giaspdvcongichct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Capdo",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Magoc",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maso",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capdo",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.DropColumn(
                name: "Magoc",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.DropColumn(
                name: "Maso",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.DropColumn(
                name: "Ten",
                table: "GiaSpDvCongIchCt");
        }
    }
}
