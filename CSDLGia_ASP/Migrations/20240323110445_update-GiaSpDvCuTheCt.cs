using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updateGiaSpDvCuTheCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sapxep",
                table: "GiaSpDvCuTheCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaSpDvCuTheCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaSpDvCuTheCt");
        }
    }
}
