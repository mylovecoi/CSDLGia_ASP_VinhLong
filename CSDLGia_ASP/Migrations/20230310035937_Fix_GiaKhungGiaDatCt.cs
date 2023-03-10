using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class Fix_GiaKhungGiaDatCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loaixa",
                table: "GiaKhungGiaDatCt");

            migrationBuilder.RenameColumn(
                name: "Giatt",
                table: "GiaKhungGiaDatCt",
                newName: "Giatttd");

            migrationBuilder.RenameColumn(
                name: "Giatd",
                table: "GiaKhungGiaDatCt",
                newName: "Giattmn");

            migrationBuilder.AddColumn<double>(
                name: "Giatddb",
                table: "GiaKhungGiaDatCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giatdmn",
                table: "GiaKhungGiaDatCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giatdtd",
                table: "GiaKhungGiaDatCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giattdb",
                table: "GiaKhungGiaDatCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Giatddb",
                table: "GiaKhungGiaDatCt");

            migrationBuilder.DropColumn(
                name: "Giatdmn",
                table: "GiaKhungGiaDatCt");

            migrationBuilder.DropColumn(
                name: "Giatdtd",
                table: "GiaKhungGiaDatCt");

            migrationBuilder.DropColumn(
                name: "Giattdb",
                table: "GiaKhungGiaDatCt");

            migrationBuilder.RenameColumn(
                name: "Giatttd",
                table: "GiaKhungGiaDatCt",
                newName: "Giatt");

            migrationBuilder.RenameColumn(
                name: "Giattmn",
                table: "GiaKhungGiaDatCt",
                newName: "Giatd");

            migrationBuilder.AddColumn<string>(
                name: "Loaixa",
                table: "GiaKhungGiaDatCt",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
