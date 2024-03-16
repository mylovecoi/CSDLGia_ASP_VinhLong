using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddUpdateGiaDatDiaBan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoQDTT",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "MaLoaiDat",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoQDTT",
                table: "GiaDatDiaBan");
            migrationBuilder.DropColumn(
               name: "GhiChu",
               table: "GiaDatDiaBan");
            migrationBuilder.DropColumn(
               name: "MaLoaiDat",
               table: "GiaDatDiaBan");
        }
    }
}
