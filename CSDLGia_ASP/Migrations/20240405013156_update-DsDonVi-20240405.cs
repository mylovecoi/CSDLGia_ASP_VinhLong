using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updateDsDonVi20240405 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CongBo",
                table: "DsDonVi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiaBanApDung",
                table: "DsDonVi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "XetDuyet",
                table: "DsDonVi",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "QuanTri",
                table: "DsDonVi",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "NhapLieu",
                table: "DsDonVi",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CongBo",
                table: "DsDonVi");

            migrationBuilder.DropColumn(
                name: "DiaBanApDung",
                table: "DsDonVi");

            migrationBuilder.DropColumn(
                name: "XetDuyet",
                table: "DsDonVi");
            migrationBuilder.DropColumn(
                name: "QuanTri",
                table: "DsDonVi");
            migrationBuilder.DropColumn(
                name: "NhapLieu",
                table: "DsDonVi");
        }
    }
}
