using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1160spdvcongichct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Mucgia3",
                table: "GiaSpDvCongIchCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgia4",
                table: "GiaSpDvCongIchCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mucgia3",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.DropColumn(
                name: "Mucgia4",
                table: "GiaSpDvCongIchCt");
        }
    }
}
