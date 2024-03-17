using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1115giaspdvcongichdm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cap1",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Cap2",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Cap3",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Cap4",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Capdo",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Hientrang",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Magoc",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Maso",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Mota",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Mucgiaden",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Mucgiatu",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Phanloai",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.DropColumn(
                name: "Ten",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.AddColumn<int>(
                name: "Sapxep",
                table: "GiaSpDvCongIchDm",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaSpDvCongIchDm");

            migrationBuilder.AddColumn<string>(
                name: "Cap1",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap2",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap3",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap4",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Capdo",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hientrang",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Magoc",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maso",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Mucgiaden",
                table: "GiaSpDvCongIchDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgiatu",
                table: "GiaSpDvCongIchDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Phanloai",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
