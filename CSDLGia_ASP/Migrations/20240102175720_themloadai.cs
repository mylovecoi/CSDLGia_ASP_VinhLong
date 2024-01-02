using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class themloadai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "test",
                table: "GiaDatPhanLoaiCt",
                newName: "Loaidat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Loaidat",
                table: "GiaDatPhanLoaiCt",
                newName: "test");
        }
    }
}
