using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1113giaspdvcongich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
