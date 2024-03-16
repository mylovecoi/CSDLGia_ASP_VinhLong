using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiancsh2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "GiaPhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "GiaPhiLePhiCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaPhiLePhiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "GiaNuocShCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienthi",
                table: "GiaNuocShCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapxep",
                table: "GiaNuocShCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaNuocShCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaPhiLePhiCt");

            migrationBuilder.DropColumn(
                name: "Madv",
                table: "GiaNuocShCt");

            migrationBuilder.DropColumn(
                name: "STTHienthi",
                table: "GiaNuocShCt");

            migrationBuilder.DropColumn(
                name: "STTSapxep",
                table: "GiaNuocShCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaNuocShCt");
        }
    }
}
