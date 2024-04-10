using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class update_dsdiaban_dsdonvi_permission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaCqcq",
                table: "DsDonVi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaDiaBanCq",
                table: "DsDiaBan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Public",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "MaCqcq",
                table: "DsDonVi");

            migrationBuilder.DropColumn(
                name: "MaDiaBanCq",
                table: "DsDiaBan");
        }
    }
}
