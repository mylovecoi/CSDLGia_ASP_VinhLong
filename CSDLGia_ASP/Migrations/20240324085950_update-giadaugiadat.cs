using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiadaugiadat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sotobando",
                table: "GiaDauGiaDatCt");

            migrationBuilder.AddColumn<double>(
                name: "Giasddat",
                table: "GiaDauGiaDatCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Giasddat",
                table: "GiaDauGiaDatCt");

            migrationBuilder.AddColumn<string>(
                name: "Sotobando",
                table: "GiaDauGiaDatCt",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
