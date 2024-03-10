using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1114giaspdvcongich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Capdo",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Magoc",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maso",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capdo",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Magoc",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Maso",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Ten",
                table: "GiaSpDvCongIchDm");
        }
    }
}
