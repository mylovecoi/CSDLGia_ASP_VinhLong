using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedvkcb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AddColumn<string>(
                name: "Tenspdv",
                table: "GiaSpDvKhungGiaCt",
                type: "nvarchar(max)",
                nullable: true);*/

            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "GiaDvKcbCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "Tenspdv",
                table: "GiaSpDvKhungGiaCt");*/

            migrationBuilder.DropColumn(
                name: "Madv",
                table: "GiaDvKcbCt");
        }
    }
}
