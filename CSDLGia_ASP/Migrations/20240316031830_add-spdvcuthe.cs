using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addspdvcuthe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sapxep",
                table: "GiaSpDvCuTheDm",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaSpDvCuTheCt",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Tt",
                table: "GiaSpDvCuTheCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Mucgia1",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Mucgia2",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.DropColumn(
                name: "Tt",
                table: "GiaSpDvCuTheCt");

            migrationBuilder.AlterColumn<string>(
                name: "Sapxep",
                table: "GiaSpDvCuTheDm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
