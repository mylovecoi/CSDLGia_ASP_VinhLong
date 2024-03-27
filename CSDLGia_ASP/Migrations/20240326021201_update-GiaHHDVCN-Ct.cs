using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updateGiaHHDVCNCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Dongia",
                table: "GiaHhDvCnCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dongia",
                table: "GiaHhDvCnCt",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
