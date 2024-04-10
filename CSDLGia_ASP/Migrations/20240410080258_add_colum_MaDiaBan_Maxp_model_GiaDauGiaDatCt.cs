using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class add_colum_MaDiaBan_Maxp_model_GiaDauGiaDatCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaDiaBan",
                table: "GiaDauGiaDatCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maxp",
                table: "GiaDauGiaDatCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaDiaBan",
                table: "GiaDauGiaDatCt");

            migrationBuilder.DropColumn(
                name: "Maxp",
                table: "GiaDauGiaDatCt");
        }
    }
}
