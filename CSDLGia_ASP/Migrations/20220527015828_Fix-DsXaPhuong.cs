using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixDsXaPhuong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Madv",
                table: "DsXaPhuong");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "DsXaPhuong",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
