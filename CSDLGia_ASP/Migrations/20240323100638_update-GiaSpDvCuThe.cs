using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updateGiaSpDvCuThe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Mota",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Phanloaidv",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.AddColumn<double>(
                name: "Mucgia1",
                table: "GiaSpDvCuTheCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgia2",
                table: "GiaSpDvCuTheCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgia3",
                table: "GiaSpDvCuTheCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgia4",
                table: "GiaSpDvCuTheCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgia5",
                table: "GiaSpDvCuTheCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgia6",
                table: "GiaSpDvCuTheCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mucgia1",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Mucgia2",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Mucgia3",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Mucgia4",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                 name: "Mucgia5",
                 table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Mucgia6",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "GiaSpDvCuTheCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phanloaidv",
                table: "GiaSpDvCuTheCt",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
