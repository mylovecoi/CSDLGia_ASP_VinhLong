using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updateGiaSpDvToiDa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gia",
                table: "GiaSpDvToiDaDm");

            migrationBuilder.RenameColumn(
                name: "Phanloai",
                table: "GiaSpDvToiDaDm",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "GiaSpDvToiDaDm",
                newName: "HienThi");

            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "GiaSpDvToiDaCt",
                newName: "Tenspdv");

            migrationBuilder.AddColumn<int>(
                name: "Sapxep",
                table: "GiaSpDvToiDaDm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaSpDvToiDaCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sapxep",
                table: "GiaSpDvToiDaCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaSpDvToiDaCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaSpDvToiDaDm");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaSpDvToiDaCt");

            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaSpDvToiDaCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaSpDvToiDaCt");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "GiaSpDvToiDaDm",
                newName: "Phanloai");

            migrationBuilder.RenameColumn(
                name: "HienThi",
                table: "GiaSpDvToiDaDm",
                newName: "Mota");

            migrationBuilder.RenameColumn(
                name: "Tenspdv",
                table: "GiaSpDvToiDaCt",
                newName: "Mota");

            migrationBuilder.AddColumn<double>(
                name: "Gia",
                table: "GiaSpDvToiDaDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
