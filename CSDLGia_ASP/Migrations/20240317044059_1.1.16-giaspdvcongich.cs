using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1116giaspdvcongich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mota",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.DropColumn(
                name: "Phanloaidv",
                table: "GiaSpDvCongIchCt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phanloaidv",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
