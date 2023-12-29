using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiaxaydungmoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cap1",
                table: "GiaXayDungMoiCt");

            migrationBuilder.DropColumn(
                name: "Cap2",
                table: "GiaXayDungMoiCt");

            migrationBuilder.DropColumn(
                name: "Cap3",
                table: "GiaXayDungMoiCt");

            migrationBuilder.DropColumn(
                name: "Cap4",
                table: "GiaXayDungMoiCt");

            migrationBuilder.DropColumn(
                name: "Cap5",
                table: "GiaXayDungMoiCt");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "GiaXayDungMoiCt");

            migrationBuilder.AlterColumn<string>(
                name: "Gia",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Gia",
                table: "GiaXayDungMoiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap1",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap2",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap3",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap4",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap5",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "GiaXayDungMoiCt",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
