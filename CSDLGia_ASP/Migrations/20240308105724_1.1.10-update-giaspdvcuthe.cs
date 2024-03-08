using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class _1110updategiaspdvcuthe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gia",
                table: "GiaSpDvCuTheDm",
                newName: "Mucgiatu");

            migrationBuilder.RenameColumn(
                name: "Mucgia",
                table: "GiaSpDvCuTheCt",
                newName: "Mucgiatu");

            migrationBuilder.AddColumn<double>(
                name: "Mucgiaden",
                table: "GiaSpDvCuTheDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mucgiaden",
                table: "GiaSpDvCuTheCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mucgiaden",
                table: "GiaSpDvCuTheDm");

            migrationBuilder.DropColumn(
                name: "Mucgiaden",
                table: "GiaSpDvCuTheCt");

           

            migrationBuilder.RenameColumn(
                name: "Mucgiatu",
                table: "GiaSpDvCuTheDm",
                newName: "Gia");

            migrationBuilder.RenameColumn(
                name: "Mucgiatu",
                table: "GiaSpDvCuTheCt",
                newName: "Mucgia");
        }
    }
}
