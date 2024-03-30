using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixModelPhiLePhiCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "PhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tenspdv",
                table: "PhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "PhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Madv",
                table: "PhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "Tenspdv",
                table: "PhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "PhiLePhiCt");
        }
    }
}
