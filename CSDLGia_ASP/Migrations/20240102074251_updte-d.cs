using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loaidat",
                table: "GiaDatPhanLoaiCt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Loaidat",
                table: "GiaDatPhanLoaiCt",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
