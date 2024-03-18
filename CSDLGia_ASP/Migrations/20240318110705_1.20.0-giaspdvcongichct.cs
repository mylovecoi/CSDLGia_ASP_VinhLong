using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1200giaspdvcongichct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SapXep",
                table: "GiaSpDvCongIchCt",
                newName: "Sapxep");

            migrationBuilder.AlterColumn<int>(
                name: "Sapxep",
                table: "GiaSpDvCongIchCt",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sapxep",
                table: "GiaSpDvCongIchCt",
                newName: "SapXep");

            migrationBuilder.AlterColumn<double>(
                name: "SapXep",
                table: "GiaSpDvCongIchCt",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
