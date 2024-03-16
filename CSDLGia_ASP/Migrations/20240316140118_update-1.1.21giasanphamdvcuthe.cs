using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class update1121giasanphamdvcuthe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Noidung",
                table: "GiaSpDvCuThe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaDvKcbCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Noidung",
                table: "GiaSpDvCuThe");

            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaDvKcbCt");
        }
    }
}
