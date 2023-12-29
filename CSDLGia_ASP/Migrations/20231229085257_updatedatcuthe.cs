using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedatcuthe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Diagioiden",
                table: "GiaDatPhanLoaiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Diagioitu",
                table: "GiaDatPhanLoaiCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagioiden",
                table: "GiaDatPhanLoaiCt");

            migrationBuilder.DropColumn(
                name: "Diagioitu",
                table: "GiaDatPhanLoaiCt");
        }
    }
}
