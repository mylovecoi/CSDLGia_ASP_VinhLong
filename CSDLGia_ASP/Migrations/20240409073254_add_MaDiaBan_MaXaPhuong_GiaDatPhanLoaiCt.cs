using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class add_MaDiaBan_MaXaPhuong_GiaDatPhanLoaiCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaDiaBan",
                table: "GiaDatPhanLoaiCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaXaPhuong",
                table: "GiaDatPhanLoaiCt",
                type: "nvarchar(max)",
                nullable: true);           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaDiaBan",
                table: "GiaDatPhanLoaiCt");

            migrationBuilder.DropColumn(
                name: "MaXaPhuong",
                table: "GiaDatPhanLoaiCt");           
        }
    }
}
