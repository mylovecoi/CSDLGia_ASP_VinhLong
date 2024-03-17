using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiadvkcbexcel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HienthiTT37",
                table: "GiaDvKcbCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HienthiTT37",
                table: "GiaDvKcbCt");
        }
    }
}
