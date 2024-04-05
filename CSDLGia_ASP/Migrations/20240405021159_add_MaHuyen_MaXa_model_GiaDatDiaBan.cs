using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class add_MaHuyen_MaXa_model_GiaDatDiaBan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaHuyen",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaXa",
                table: "GiaDatDiaBan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaHuyen",
                table: "GiaDatDiaBan");

            migrationBuilder.DropColumn(
                name: "MaXa",
                table: "GiaDatDiaBan");
        }
    }
}
