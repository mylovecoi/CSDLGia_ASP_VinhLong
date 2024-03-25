using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixGiaThueDNCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TyLe1",
                table: "GiaThueMatDatMatNuocCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TyLe2",
                table: "GiaThueMatDatMatNuocCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TyLe3",
                table: "GiaThueMatDatMatNuocCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TyLe1",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "TyLe2",
                table: "GiaThueMatDatMatNuocCt");

            migrationBuilder.DropColumn(
                name: "TyLe3",
                table: "GiaThueMatDatMatNuocCt");
        }
    }
}
