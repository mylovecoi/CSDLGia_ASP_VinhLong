using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedb118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ipf1",
                table: "GiaThueMatDatMatNuoc",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ipf1",
                table: "GiaThueMatDatMatNuoc");
        }
    }
}
