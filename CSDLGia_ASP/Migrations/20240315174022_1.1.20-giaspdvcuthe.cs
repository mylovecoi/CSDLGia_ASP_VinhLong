using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1120giaspdvcuthe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hientrang",
                table: "GiaSpDvCuTheDm");

            migrationBuilder.RenameColumn(
                name: "Phanloai",
                table: "GiaSpDvCuTheDm",
                newName: "Tt");

            migrationBuilder.RenameColumn(
                name: "Mucgiatu",
                table: "GiaSpDvCuTheDm",
                newName: "Mucgia2");

            migrationBuilder.RenameColumn(
                name: "Mucgiaden",
                table: "GiaSpDvCuTheDm",
                newName: "Mucgia1");

            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "GiaSpDvCuTheDm",
                newName: "Sapxep");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tt",
                table: "GiaSpDvCuTheDm",
                newName: "Phanloai");

            migrationBuilder.RenameColumn(
                name: "Sapxep",
                table: "GiaSpDvCuTheDm",
                newName: "Mota");

            migrationBuilder.RenameColumn(
                name: "Mucgia2",
                table: "GiaSpDvCuTheDm",
                newName: "Mucgiatu");

            migrationBuilder.RenameColumn(
                name: "Mucgia1",
                table: "GiaSpDvCuTheDm",
                newName: "Mucgiaden");

            migrationBuilder.AddColumn<string>(
                name: "Hientrang",
                table: "GiaSpDvCuTheDm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
