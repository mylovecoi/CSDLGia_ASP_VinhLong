using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class add_colum_TrangThai_MaDv_model_GiaDauGiaDatCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaDv",
                table: "GiaDauGiaDatCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "GiaDauGiaDatCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaDv",
                table: "GiaDauGiaDatCt");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "GiaDauGiaDatCt");
        }
    }
}
