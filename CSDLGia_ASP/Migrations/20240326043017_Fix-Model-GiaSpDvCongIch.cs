using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixModelGiaSpDvCongIch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sapxep",
                table: "GiaSpDvCongIchCt",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaSpDvCongIchCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Madv",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaSpDvCongIchCt");

            migrationBuilder.AlterColumn<double>(
                name: "Sapxep",
                table: "GiaSpDvCongIchCt",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
